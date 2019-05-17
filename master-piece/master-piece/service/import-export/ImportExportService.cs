using System;
using System.IO;
using System.Windows.Forms;

namespace master_piece.service.import_export
{
    /// <summary>
    /// Сервис импорта и экспорта
    /// </summary>
    class ImportExportService
    {
        /// <summary>
        /// Разделитель в результирующем текстовом файле
        /// </summary>
        private static readonly string DELIMITER = "\t\t";

        /// <summary>
        /// Метод импорта переменных.
        /// Возвращает сущность <see cref="ImportExportResult"/>.
        /// </summary>
        /// <param name="dataGridView">Таблица переменных с формы MainForm</param>
        public static ImportExportResult importVariables(DataGridView dataGridView)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dataGridView.Rows.Clear();

                string filePath = openFileDialog.FileName;
                Stream fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string line;
                    int lineCounter = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineCounter++;
                        string[] values = line.Split(new string[] { DELIMITER }, StringSplitOptions.RemoveEmptyEntries);
                        if (values.Length == 2)
                        {
                            dataGridView.Rows.Add(values[0], values[1]);
                        }
                        else
                        {
                            return new ImportExportResult(
                                ImportExportResultStatus.Error, 
                                "Файл переменных некорректен.\nНайдена ошибка в " + lineCounter + " строке"
                            );
                        }
                    }
                }
                return new ImportExportResult(ImportExportResultStatus.Success, null);
            }
            else
            {
                return new ImportExportResult(ImportExportResultStatus.Canceled, null);
            }
        }

        /// <summary>
        /// Метод импорта выражений.
        /// Возвращает сущность <see cref="ImportExportResult"/>.
        /// </summary>
        /// <param name="dataGridView">Таблица выражений с формы MainForm</param>
        public static ImportExportResult importExpressions(DataGridView dataGridView)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dataGridView.Rows.Clear();

                string filePath = openFileDialog.FileName;
                Stream fileStream = openFileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string line;
                    int lineCounter = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineCounter++;
                        string[] values = line.Split(new string[] { DELIMITER }, StringSplitOptions.RemoveEmptyEntries);
                        if (values.Length == 2)
                        {
                            dataGridView.Rows.Add(values[0], values[1]);
                        }
                        else if (values.Length == 3)
                        {
                            dataGridView.Rows.Add(values[0], values[1], values[2]);
                        }
                        else
                        {
                            return new ImportExportResult(
                                ImportExportResultStatus.Error,
                                "Файл выражений некорректен.\nНайдена ошибка в " + lineCounter + " строке"
                            );
                        }
                    }
                }
                return new ImportExportResult(ImportExportResultStatus.Success, null);
            }
            else
            {
                return new ImportExportResult(ImportExportResultStatus.Canceled, null);
            }
        }

        /// <summary>
        /// Метод экспорта переменных.
        /// Возвращает сущность <see cref="ImportExportResult"/>.
        /// </summary>
        /// <param name="dataGridView">Таблица переменных с формы MainForm</param>
        public static ImportExportResult exportVariables(DataGridView dataGridView)
        {
            if (dataGridView.NewRowIndex == 0)
            {
                return new ImportExportResult(ImportExportResultStatus.Error, "Таблица переменных пуста");
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы|*.txt";
            saveFileDialog.RestoreDirectory = true;
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream saveStream = saveFileDialog.OpenFile();
                using (StreamWriter writer = new StreamWriter(saveStream))
                {
                    int lineCounter = 0;
                    foreach (DataGridViewRow dgvr in dataGridView.Rows)
                    {
                        if (dgvr.Index == dataGridView.NewRowIndex)
                        {
                            break;
                        }

                        lineCounter++;
                        if ((dgvr.Cells.Count == 2) && (dgvr.Cells[0].Value != null) && (dgvr.Cells[1].Value != null))
                        {
                            writer.WriteLine(dgvr.Cells[0].Value + DELIMITER + dgvr.Cells[1].Value);
                        }
                        else
                        {
                            return new ImportExportResult(
                                ImportExportResultStatus.Error,
                                "Ошибка в " + lineCounter + " строке таблицы переменных:\nНе все ячейки таблицы заполнены."
                            );
                        }
                    }
                }
                return new ImportExportResult(ImportExportResultStatus.Success, "Таблица переменных успешно экспортирована");
            }
            else
            {
                return new ImportExportResult(ImportExportResultStatus.Canceled, null);
            }
        }

        /// <summary>
        /// Метод экспорта выражений.
        /// Возвращает сущность <see cref="ImportExportResult"/>.
        /// </summary>
        /// <param name="dataGridView">Таблица выражений с формы MainForm</param>
        public static ImportExportResult exportExpressions(DataGridView dataGridView)
        {
            if (dataGridView.NewRowIndex == 0)
            {
                return new ImportExportResult(ImportExportResultStatus.Error, "Таблица выражений пуста");
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы|*.txt";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream saveStream = saveFileDialog.OpenFile();
                using (StreamWriter writer = new StreamWriter(saveStream))
                {
                    int lineCounter = 0;
                    foreach (DataGridViewRow dgvr in dataGridView.Rows)
                    {
                        if (dgvr.Index == dataGridView.NewRowIndex)
                        {
                            break;
                        }

                        lineCounter++;
                        if ((dgvr.Cells.Count == 3) && (dgvr.Cells[0].Value != null) && (dgvr.Cells[1].Value != null))
                        {
                            if (dgvr.Cells[2].Value != null)
                            {
                                writer.WriteLine(dgvr.Cells[0].Value + DELIMITER + dgvr.Cells[1].Value + DELIMITER + dgvr.Cells[2].Value);
                            }
                            else
                            {
                                writer.WriteLine(dgvr.Cells[0].Value + DELIMITER + dgvr.Cells[1].Value);
                            }
                        }
                        else
                        {
                            return new ImportExportResult(
                                ImportExportResultStatus.Error,
                                "Ошибка в " + lineCounter + " строке таблицы выражений:\nНе заполнена ячейка \"ЕСЛИ\" или \"ТО\"."
                            );
                        }
                    }
                }
                return new ImportExportResult(ImportExportResultStatus.Success, "Таблица выражений успешно экспортирована");
            }
            else
            {
                return new ImportExportResult(ImportExportResultStatus.Canceled, null);
            }
        }

        /// <summary>
        /// Метод экспорта результатов обработки нечетких правил.
        /// Возвращает сущность <see cref="ImportExportResult"/>.
        /// </summary>
        /// <param name="richTextBox">RichTextBox с результатами, расположенный на форме MainForm</param>
        public static ImportExportResult exportResults(RichTextBox richTextBox)
        {
            if (richTextBox.Lines.Length == 0)
            {
                return new ImportExportResult(ImportExportResultStatus.Error, "Нет результатов для экспорта");
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы|*.txt";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream saveStream = saveFileDialog.OpenFile();
                using (StreamWriter writer = new StreamWriter(saveStream))
                {
                    foreach (string line in richTextBox.Lines)
                    {
                        writer.WriteLine(line);
                    }
                }
                return new ImportExportResult(ImportExportResultStatus.Success, "Результаты успешно экспортированы");
            }
            else
            {
                return new ImportExportResult(ImportExportResultStatus.Canceled, null);
            }
        }
    }
}
