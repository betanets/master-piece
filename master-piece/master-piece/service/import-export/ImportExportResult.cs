namespace master_piece.service.import_export
{
    /// <summary>
    /// Сущность результата импорта или экспорта
    /// </summary>
    class ImportExportResult
    {
        /// <summary>
        /// Статус импорта или экспорта
        /// </summary>
        public ImportExportResultStatus status;

        /// <summary>
        /// Сообщение с результатом импорта или экспорта.
        /// Сообщение - null, если статус - Canceled или Success
        /// </summary>
        public string messageString;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="status">Статус импорта или экспорта</param>
        /// <param name="messageString">Сообщение с результатом импорта или экспорта</param>
        public ImportExportResult(ImportExportResultStatus status, string messageString)
        {
            this.status = status;
            this.messageString = messageString;
        }
    }
}
