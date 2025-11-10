namespace Api.Helpers
{
    public static class ResponseHelper
    {
        // Clase base de respuesta genérica
        public class ApiResponse<T>
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
            public T? Data { get; set; }
        }

        // Método para respuestas exitosas
        public static ApiResponse<T> SuccessResponse<T>(T data, string message = "Operación exitosa")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        //  Método para errores
        public static ApiResponse<T> ErrorResponse<T>(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }

        //  Método para cuando algo no se encuentra
        public static ApiResponse<T> NotFoundResponse<T>(string message = "No se encontraron resultados")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }
}
