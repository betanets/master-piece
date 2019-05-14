using System.Collections.Generic;

namespace master_piece.service
{
    /// <summary>
    /// Сущность результата лексического анализа
    /// </summary>
    class LexicalAnalysisResult
    {
        /// <summary>
        /// Флаг, указывающий, успешно ли выполнен ли лексический анализ успешно.
        /// true, если анализ успешно выполнен, false в противном случае
        /// </summary>
        public bool isCorrect { get; set; }

        /// <summary>
        /// Список ошибок и предупреждений, полученных в результате лекического анализа
        /// </summary>
        public List<string> output { get; set; }
    }
}
