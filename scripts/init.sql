-- Script de inicialização do banco de desenvolvimento
-- Este script é executado automaticamente quando o container PostgreSQL é criado

-- Criar extensões necessárias
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Configurar timezone
SET timezone = 'America/Sao_Paulo';

-- Criar usuário adicional se necessário
-- CREATE USER app_user WITH PASSWORD 'app_password';
-- GRANT ALL PRIVILEGES ON DATABASE ulcera_venosa_dev TO app_user;

-- Log de inicialização
SELECT 'Banco de desenvolvimento inicializado com sucesso!' as status;
