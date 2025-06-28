using System.Text.Json.Serialization;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Core {
    public sealed class Error {
        public Error(params string[] message)
        {
            Message.AddRange(message);
        }

        public List<string> Message { get; } = new List<string>();     
        public void Add(string message) {
            Message.Add(message);
        }

        [JsonIgnore]
        public bool HasAnyMessage { get => this.Message.Count > 0; }
    };

    public class Result<TValue, TError> {

        public TValue? Left { get; }
        public TError? Right { get; }

        private bool _isSuccess;
        public bool IsSuccess => _isSuccess;
        private Result(TValue value) {
            _isSuccess = true;
            Left = value;
            Right = default;
        }

        private Result(TError error) {
            _isSuccess = false;
            Left = default;
            error = error;
        }
               
        public static implicit operator Result<TValue, TError>(TValue value) => new Result<TValue, TError>(value);
               
        public static implicit operator Result<TValue, TError>(TError error) => new Result<TValue, TError>(error);

        public Result<TValue, TError> Match(Func<TValue, Result<TValue, TError>> success,
            Func<TError, Result<TValue, TError>> failure) {
            if (_isSuccess) {
                return success(Left!);
            }
            return failure(Right!);
        }
    }
}
