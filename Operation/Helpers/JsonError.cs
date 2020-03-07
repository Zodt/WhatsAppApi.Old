namespace WhatsAppApi.Operation.Helpers
{
    //:Done
    /// <summary>
    /// Класс ошибок при сериализации/десериализации
    /// </summary>
    public class JsonError
    {
        public JsonError() { }

        public JsonError(bool isError, int codeOfError, string textError)
        {
            IsError = isError;
            CodeOfError = codeOfError;
            TextError = textError;
        }

        ~JsonError()
        {
            IsError = default;
            CodeOfError = default;
            TextError = default;
        }

        /// <summary>
        /// Флаг наличия ошибки
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// Код ошибки
        /// </summary>
        public int CodeOfError { get; set; }
        /// <summary>
        /// Описание ошибки
        /// </summary>
        public string TextError { get; set; }

        /// <summary>
        /// Предбразование всех данных в классе в строковой тип
        /// </summary>
        public override string ToString() => IsError ? $"Ошибка, код {CodeOfError}: {TextError}" : TextError;
    }
}