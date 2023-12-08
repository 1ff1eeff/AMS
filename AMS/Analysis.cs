using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AMS
{    
    public partial class Analysis : Form
    {
        // Для связи с главной формой. Список состояний узлов.

        public List<AmsNodeStat> nodesStatus = new List<AmsNodeStat>();

        // Для связи с главной формой. Настройки приложения.

        public AmsSettings amsSettings = new AmsSettings();

        // Инициализация компонентов.

        public Analysis()
        {
            InitializeComponent();            
        }

        // Кнопка "Закрыть".
        private void button1_Click(object sender, EventArgs e)
        {
            // Закрываем окно.

            Close();
        }

        // Событие – "Форма загружена".

        private void Analysis_Load(object sender, EventArgs e)
        {
            // Сегодняшняя дата для формирования названия файла.

            string today =  DateTime.Now.Date.Day.ToString()
                    + "-" + DateTime.Now.Date.Month.ToString()
                    + "-" + DateTime.Now.Date.Year.ToString();

            // Проверяем наличие директории для хранения логов.
            // При её отсутствии – создаём новую папку.

            string logsDir = Directory.CreateDirectory(amsSettings.LogsFolder + "\\").FullName;

            // Каждый день формируем новое название лог-файла.

            string todayLogFile = logsDir + today + ".xml";

            // Открываем файл и заполняем список.

            if (File.Exists(todayLogFile))
            {
                // Обновляем статистику узлов. 

                UpdateNodesStatistics(lvStatistics, todayLogFile);
            }

            // Ищем архивные log-файлы.

            string[] files = Directory.GetFiles(logsDir);

            // Отображаем имена найденных файлов.

            foreach (string file in files)
            {
                // Добавляем в ComboBox имя файла.

                cbLogFile.Items.Add(file);
            }
        }

        // Событие – "Индекс изменился" при выборе нового элемента в ComboBox.

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Изменяем имя файла на текст выбранного элемента в ComboBox.

            string filename = cbLogFile.Text;

            // Очищаем список "Общая статистика работы узлов".

            lvStatistics.Items.Clear();

            // Обновляем список "Общая статистика работы узлов".

            UpdateNodesStatistics(lvStatistics, filename);
        }

        /// <summary>
        /// Обновить список статистики узлов.
        /// </summary>
        /// <param name="lv">Целевой элемент ListView.</param>
        /// <param name="filename">Расположение XML-файла.</param>
        private void UpdateNodesStatistics(ListView lv, string filename)
        {
            // Получаем информацию о состоянии узлов из XML файла.
            // Тип данных для сериализации – List<AmsNodeStat>.

            XmlSerializer formatter = new XmlSerializer(typeof(List<AmsNodeStat>));

            // Открываем файл хранящий информацию о состоянии узлов.
            // Имя файла получено из входных параметров функции.

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                try
                {
                    // Заполняем элемент формы ListView.

                    cbLogFile.Text = filename;

                    // Заполняем список статистики состояния узлов данными из файла.
                    // Если файл был успешно десериализован и содержит информацию.

                    if (formatter.Deserialize(fs) is List<AmsNodeStat> stats && stats.Count > 0)
                    {
                        // Анализируем каждый элемент в списке статистики состояния узлов.

                        foreach (AmsNodeStat stat in stats)
                        {
                            // Создаём новый объект ListViewItem и заполняем его данными,
                            // полученными из списка статистики состояния узлов.

                            ListViewItem item = new ListViewItem();

                            // Имя узла

                            item.Text = stat.Name;

                            // Адрес узла

                            item.SubItems.Add(stat.Ip);

                            // Успешно

                            item.SubItems.Add(stat.Succed.ToString());

                            // Отказов

                            item.SubItems.Add(stat.Failed.ToString());

                            // Время простоя

                            item.SubItems.Add(GenTimeFromSeconds(stat.OfflineTime));

                            // Время работы

                            item.SubItems.Add(GenTimeFromSeconds(stat.OnlineTime));

                            // Процент простоя

                            item.SubItems.Add(stat.OfflinePercent.ToString() + "%");

                            // Начало опроса

                            item.SubItems.Add(stat.Time.ToString());

                            // Финал опроса

                            item.SubItems.Add(stat.FinishTime.ToString());

                            // Добавляем созданный объект ListViewItem в элемент ListView формы.

                            lv.Items.Add(item);
                        }
                    }
                }

                // Если в процессе обновления информации возникли проблемы.

                catch (Exception e) 
                {
                    // Уведомляем пользователя посредством диалогового окна.

                    MessageBox.Show(e.Message);
                }
            }
        }

        /// <summary>
        /// Преобразовать секунды в форматированную строку.
        /// </summary>
        /// <param name="seconds">Время в секундах.</param>
        /// <returns>Время в формате "чч:мм:сс".</returns>
        private string GenTimeFromSeconds(double seconds)
        {
            // Создаём строку формата "чч:мм:сс" (00:00:00) на основаннии данных
            // о количестве секунд, полученных из входного параметра – "seconds".

            string timeInterval = TimeSpan.FromSeconds(seconds).ToString();

            // Возвращаем строку, содержащую время в формате чч:мм:сс.

            return timeInterval;
        }

        // Копка "Открыть".
        private void button2_Click(object sender, EventArgs e)
        {
            // Проверяем наличие директории для хранения логов.
            // При её отсутствии – создаём новую папку.

            Directory.CreateDirectory(amsSettings.LogsFolder);

            // Открывать по умолчанию папку с логами.

            openFileDialog1.InitialDirectory = amsSettings.LogsFolder;

            // Отображаем диалог выбора лог-файла.
            // Если пользователь не отменил открытие лог-файла. 

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                // Если выбранный файл уже есть в списке компонента формы ComboBox.

                if (cbLogFile.Items.Contains(openFileDialog1.FileName))
                {
                    // Выделяем файл в списке.

                    cbLogFile.Text = openFileDialog1.FileName;
                }

                // Если выбранного файла нет в списке компонента формы ComboBox.

                else
                {
                    // Добавляем и выделяем файл в списке.

                    cbLogFile.SelectedIndex = cbLogFile.Items.Add(openFileDialog1.FileName);
                }
            }
        }
    }
}
