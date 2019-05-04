namespace master_piece.service.import_export
{
    /// <summary>
    /// Result of import or export
    /// </summary>
    class ImportExportResult
    {
        public ImportExportResultStatus status;
        public string messageString;

        public ImportExportResult(ImportExportResultStatus status, string messageString)
        {
            this.status = status;
            this.messageString = messageString;
        }
    }
}
