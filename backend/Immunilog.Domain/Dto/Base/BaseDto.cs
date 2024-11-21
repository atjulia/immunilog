namespace Immunilog.Domain.Dto.Base;

public abstract class BaseDto
{
    public Guid Id { get; set; }
}

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Msg { get; set; }
    public T Data { get; set; }

    public static ApiResponse<T> SuccessResponse(T data, string msg = "Operação bem-sucedida")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Msg = msg,
            Data = data
        };
    }

    public static ApiResponse<T> FailureResponse(string msg = "Erro desconhecido")
    {
        return new ApiResponse<T>
        {
            Success = false,
            Msg = msg
        };
    }
}
