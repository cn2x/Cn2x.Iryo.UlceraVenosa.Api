# Makefile para facilitar o desenvolvimento do projeto Úlcera Venosa API

.PHONY: help dev-up dev-down dev-logs api-run db-migrate db-reset test clean

# Ajuda
help:
	@echo "Comandos disponíveis:"
	@echo "  dev-up      - Subir banco de desenvolvimento (PostgreSQL + pgAdmin)"
	@echo "  dev-down    - Parar banco de desenvolvimento"
	@echo "  dev-logs    - Ver logs do banco de desenvolvimento"
	@echo "  api-run     - Executar API localmente"
	@echo "  db-migrate  - Aplicar migrations no banco"
	@echo "  db-reset    - Resetar banco de desenvolvimento (CUIDADO: apaga dados)"
	@echo "  test        - Executar testes"
	@echo "  clean       - Limpar build artifacts"
	@echo "  full-up     - Subir aplicação completa (API + Banco + pgAdmin)"
	@echo "  full-down   - Parar aplicação completa"

# Desenvolvimento local (apenas banco)
dev-up:
	docker compose -f docker-compose.dev.yml up -d
	@echo "Banco de desenvolvimento disponível em localhost:5432"
	@echo "pgAdmin disponível em http://localhost:5050"

dev-down:
	docker compose -f docker-compose.dev.yml down

dev-logs:
	docker compose -f docker-compose.dev.yml logs -f

# API local
api-run: dev-up db-migrate
	dotnet run --project src/2-Presentation/Cn2x.Iryo.UlceraVenosa.Api

# Migrations
db-migrate:
	@echo "Aplicando migrations no banco de desenvolvimento..."
	dotnet ef database update --project src/4-Infrastructure/Cn2x.Iryo.UlceraVenosa.Infrastructure --startup-project src/2-Presentation/Cn2x.Iryo.UlceraVenosa.Api --connection "Host=localhost;Database=ulcera_venosa_dev;Username=dev_user;Password=dev_password123;SSL Mode=Disable"

db-reset:
	@echo "ATENÇÃO: Este comando irá apagar todos os dados do banco de desenvolvimento!"
	@read -p "Tem certeza? [y/N] " -n 1 -r; \
	echo; \
	if [[ $$REPLY =~ ^[Yy]$$ ]]; then \
		docker compose -f docker-compose.dev.yml down -v; \
		docker compose -f docker-compose.dev.yml up -d; \
		sleep 10; \
		dotnet ef database update --project src/4-Infrastructure/Cn2x.Iryo.UlceraVenosa.Infrastructure --startup-project src/2-Presentation/Cn2x.Iryo.UlceraVenosa.Api --connection "Host=localhost;Database=ulcera_venosa_dev;Username=dev_user;Password=dev_password123;SSL Mode=Disable"; \
	fi

# Aplicação completa
full-up:
	docker compose up -d
	@echo "Aplicação completa disponível:"
	@echo "  API: http://localhost:8080/graphql"
	@echo "  pgAdmin: http://localhost:5050"

full-down:
	docker compose down

# Testes
test:
	dotnet test

# Limpeza
clean:
	dotnet clean
	docker compose down -v
	docker system prune -f

# Setup inicial
setup: dev-up
	@echo "Aguardando banco inicializar..."
	sleep 10
	dotnet ef database update --project src/4-Infrastructure/Cn2x.Iryo.UlceraVenosa.Infrastructure --startup-project src/2-Presentation/Cn2x.Iryo.UlceraVenosa.Api --connection "Host=localhost;Database=ulcera_venosa_dev;Username=dev_user;Password=dev_password123;SSL Mode=Disable"
	@echo "Setup concluído! Use 'make api-run' para executar a API."
