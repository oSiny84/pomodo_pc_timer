using System.Media;

namespace PomodoTimer
{
    public partial class TimerForm : Form
    {
        private readonly System.Windows.Forms.Timer mainTimer;
        private readonly System.Windows.Forms.Timer blinkTimer;
        private DateTime endTime;
        private int state;
        private readonly string logFilePath = "TimerLog.txt";
        private DateTime startTime;
        private int blinkCount;
        private readonly Color defaultFormColor;

        public TimerForm()
        {
            InitializeComponent();

            // 메인 타이머 설정
            mainTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            mainTimer.Tick += MainTimer_Tick;

            // 깜박임 타이머 설정
            blinkTimer = new System.Windows.Forms.Timer { Interval = 500 };
            blinkTimer.Tick += BlinkTimer_Tick;

            defaultFormColor = this.BackColor;

            ResetTimer();
        }

        private void ResetTimer()
        {
            mainTimer.Stop();
            mainTimer.Start();
            startTime = DateTime.Now;

            SetTimerState();
        }

        private void SetTimerState()
        {
            int minutes;
            string message;

            switch (state)
            {
                case 0:
                    minutes = 25;
                    message = "(1) 25분 일하기..";
                    state++;
                    break;
                case 1:
                    minutes = 5;
                    message = "5분 쉬기..";
                    state++;
                    break;
                case 2:
                    minutes = 25;
                    message = "(2) 25분 일하기..";
                    state++;
                    break;
                case 3:
                    minutes = 5;
                    message = "5분 쉬기..";
                    state++;
                    break;
                case 4:
                    minutes = 25;
                    message = "(3) 25분 일하기..";
                    state++;
                    break;
                case 5:
                    minutes = 5;
                    message = "5분 쉬기..";
                    state++;
                    break;
                case 6:
                    minutes = 25;
                    message = "(4) 25분 일하기..";
                    state++;
                    break;
                case 7:
                    minutes = 30;
                    message = "30분 쉬기..";
                    state = 0;
                    break;
                default:
                    throw new InvalidOperationException("잘못된 상태 값입니다.");
            }

            endTime = DateTime.Now.AddMinutes(minutes);
            this.Text = message;
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            if (checkBox_TimerHolding.Checked)
                endTime = endTime.AddSeconds(1);

            TimeSpan remainingTime = endTime - DateTime.Now;

            if (remainingTime.TotalSeconds > 0)
            {
                textBox1.Text = $"{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";
            }
            else
            {
                HandleTimerEnd();
            }
        }

        private void HandleTimerEnd()
        {
            if ((state - 1) % 2 == 0)
            {
                LogToFile($"<타이머 시작> {startTime}");
                LogToFile($"<타이머 종료> {endTime}");
            }

            ResetTimer();
            textBox1.Text = "</타이머 종료>";
            PlayAlarm();
            StartBlinking();
        }

        private void LogToFile(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"파일 쓰기 중 오류 발생: {ex.Message}");
            }
        }

        private void PlayAlarm()
        {
            const string alarmPath = "전자시계 알람.wav";

            try
            {
                if (File.Exists(alarmPath))
                {
                    using (var player = new SoundPlayer(alarmPath))
                    {
                        player.Play();
                    }
                }
                else
                {
                    MessageBox.Show("알람음 파일을 찾을 수 없습니다: Alarm.wav", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"알람음을 재생하는 동안 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void StartBlinking()
        {
            blinkCount = 0;
            blinkTimer.Start();
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            this.BackColor = blinkCount % 2 == 0 ? Color.Red : Color.Blue;

            if (++blinkCount > 10)
            {
                blinkTimer.Stop();
                this.BackColor = defaultFormColor;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainTimer.Stop();
            blinkTimer.Stop();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBox_Top.Checked;
        }

        private void button_min_up_Click(object sender, EventArgs e)
        {
            endTime = endTime.AddMinutes(1);
        }

        private void button_min_down_Click(object sender, EventArgs e)
        {
            endTime = endTime.AddMinutes(-1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            state = 0;
            ResetTimer();
        }
    }
}