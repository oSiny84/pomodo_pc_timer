using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace TimerVisualizer
{
    public partial class MainForm : Form
    {
        private const int MinutesPerBlock = 1; // Change block to 1 minute
        private const int TotalMinutes = 24 * 60; // 1440 minutes in a day
        private const int TotalBlocks = TotalMinutes / MinutesPerBlock;

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Timer Visualizer";
            this.Size = new Size(1800, 900);

            var openFileButton = new Button
            {
                Text = "Open TXT File",
                Location = new Point(10, 10),
                Size = new Size(150, 40)
            };
            openFileButton.Click += OpenFileButton_Click;
            this.Controls.Add(openFileButton);

            _dateListBox = new ListBox
            {
                Location = new Point(10, 60),
                Size = new Size(200, 700)
            };
            _dateListBox.SelectedIndexChanged += DateListBox_SelectedIndexChanged;
            this.Controls.Add(_dateListBox);

            var timelinePanel = new Panel
            {
                Location = new Point(220, 60),
                Size = new Size(1550, 700),
                BorderStyle = BorderStyle.FixedSingle
            };
            timelinePanel.Paint += TimelinePanel_Paint;
            this.Controls.Add(timelinePanel);

            var infoLabel = new Label
            {
                Location = new Point(220, 780),
                Size = new Size(1550, 40),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            this.Controls.Add(infoLabel);
            _timelinePanel = timelinePanel;
            _infoLabel = infoLabel;
        }

        private Panel _timelinePanel;
        private Label _infoLabel;
        private ListBox _dateListBox;
        private Dictionary<string, List<(DateTime start, DateTime end)>> _groupedTimeRanges = new();
        private List<(DateTime start, DateTime end)> _selectedTimeRanges = new();

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ParseFile(openFileDialog.FileName);
                    PopulateDateList();
                    _timelinePanel.Invalidate(); // Redraw timeline panel
                    UpdateInfoLabel();
                }
            }
        }

        private void ParseFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            _groupedTimeRanges.Clear();

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("<타이머 시작>"))
                {
                    var startTimeStr = lines[i].Split(new[] { "<타이머 시작>" }, StringSplitOptions.None)[1].Trim();
                    var endTimeStr = lines[++i].Split(new[] { "<타이머 종료>" }, StringSplitOptions.None)[1].Trim();

                    if (DateTime.TryParse(startTimeStr, out var startTime) && DateTime.TryParse(endTimeStr, out var endTime))
                    {
                        var dateKey = startTime.ToString("yyyy-MM-dd");
                        if (!_groupedTimeRanges.ContainsKey(dateKey))
                        {
                            _groupedTimeRanges[dateKey] = new List<(DateTime start, DateTime end)>();
                        }
                        _groupedTimeRanges[dateKey].Add((startTime, endTime));
                    }
                }
            }

            if (!_groupedTimeRanges.Any())
            {
                MessageBox.Show("File does not contain valid timer data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateDateList()
        {
            _dateListBox.Items.Clear();
            foreach (var date in _groupedTimeRanges.Keys.OrderBy(d => d))
            {
                _dateListBox.Items.Add(date);
            }
        }

        private void DateListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dateListBox.SelectedItem is string selectedDate && _groupedTimeRanges.ContainsKey(selectedDate))
            {
                _selectedTimeRanges = _groupedTimeRanges[selectedDate];
                _timelinePanel.Invalidate(); // Redraw timeline panel for selected date
                UpdateInfoLabel();
            }
        }

        private void TimelinePanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            var panelWidth = _timelinePanel.Width;
            var panelHeight = _timelinePanel.Height;
            var blockWidth = panelWidth / 60.0f; // 60 minutes per hour
            var blockHeight = panelHeight / 24.0f; // 24 hours

            // Draw timeline blocks and hours
            for (int hour = 0; hour < 24; hour++)
            {
                for (int minute = 0; minute < 60; minute++)
                {
                    var x1 = minute * blockWidth;
                    var y1 = hour * blockHeight;
                    var rect = new RectangleF(x1, y1, blockWidth, blockHeight);
                    g.FillRectangle(Brushes.LightGray, rect);
                    g.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height);
                }
                // Draw hour labels at the start of each row
                g.DrawString($"{hour}:00", new Font("Arial", 12), Brushes.Black, new PointF(5, hour * blockHeight + blockHeight / 3));
            }

            // Highlight time ranges for selected date
            foreach (var (start, end) in _selectedTimeRanges)
            {
                var startBlock = (start.Hour * 60 + start.Minute);
                var endBlock = (end.Hour * 60 + end.Minute);

                for (int blockIndex = startBlock; blockIndex <= endBlock; blockIndex++)
                {
                    int hour = blockIndex / 60;
                    int minute = blockIndex % 60;
                    var x1 = minute * blockWidth;
                    var y1 = hour * blockHeight;
                    var rect = new RectangleF(x1, y1, blockWidth, blockHeight);
                    g.FillRectangle(Brushes.Green, rect);
                }
            }
        }

        private void UpdateInfoLabel()
        {
            if (!_selectedTimeRanges.Any())
            {
                _infoLabel.Text = "No timer data available for selected date.";
            }
            else
            {
                var info = string.Join(" | ", _selectedTimeRanges.Select(r => $"Start: {r.start:HH:mm:ss}, End: {r.end:HH:mm:ss}"));
                _infoLabel.Text = info;
            }
        }
    }
    
}