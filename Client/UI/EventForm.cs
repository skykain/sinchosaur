﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Client.UserEventsServiceReference;
using Client.ServiceReference;
using System.IO;



namespace Client
{
    public partial class EventForm : Form
    {

        // состояние прогресса синхронизации файла
        public ProgressFileInfo SinchronizeFileProgressInfo = new ProgressFileInfo();

        //форма прогресса синхронизации
        ProgressForm progressForm;

        // id выбранного файла
        public int selectedFileId;

        //--------------------------------------------------------------------------------
        public EventForm()
        {
            InitializeComponent();
            FillListView();
        }
        //--------------------------------------------------------------------------------


        //Заполняет список измененных файлов
        private void FillListView()
        {
            EventInfo [] userEvents = null;

            using (UserEventsClient client = new UserEventsClient())
            {
                userEvents = client.GetEvents(Account.GetUserEmail(), Account.GetUserPass());
            }

            listUserEvents.Items.Clear();

           
            foreach (var userEvent in userEvents)
            {
                ListViewItem item = new ListViewItem(userEvent.Description);

                item.SubItems.Add(userEvent.FileName);

                item.SubItems.Add(userEvent.Path);
                float fileSize = (float)userEvent.FileSize / 1024.0f;
                string suffix = "KБайт";

                if (fileSize > 1000.0f)
                {
                    fileSize /= 1024.0f;
                    suffix = "MБайт";
                }
                item.SubItems.Add(string.Format("{0:0.0} {1}", fileSize, suffix));

                item.SubItems.Add(userEvent.Created.ToString());
                item.SubItems.Add(userEvent.FileSize.ToString());
                item.SubItems.Add(userEvent.FileId.ToString());

                listUserEvents.Items.Add(item);
            }
        }
        //--------------------------------------------------------------------------------



        //--------------------------------------------------------------------------------
        private void buttonDownload_Click(object sender, EventArgs e)
        {
            if (listUserEvents.SelectedItems.Count == 0)
                MessageBox.Show("Вы должны выбрать файл!");
            else
            {
                ListViewItem item = listUserEvents.SelectedItems[0];
                
                string fileName = item.SubItems[1].Text;
              
                SaveFileDialog dlg = new SaveFileDialog()
                {
                    RestoreDirectory = true,
                    OverwritePrompt = true,
                    Title = "Сохранить как...",
                    FileName = fileName
                };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(dlg.FileName))
                    {
                        SinchronizeFileProgressInfo.File = new MyFile
                        {
                            Name = fileName,
                            Path = dlg.FileName,
                            Size = Convert.ToInt64(item.SubItems[5].Text),
                            IsDirectory = false,
                        };
                        selectedFileId = Convert.ToInt32(item.SubItems[6].Text);
                        progressForm = new ProgressForm(SinchronizeFileProgressInfo);
                        backgroundDownloader.RunWorkerAsync(SinchronizeFileProgressInfo.File);
                    }
                }
            }
        }
        //--------------------------------------------------------------------------------


        //сохранение файла
        private void backgroundDownloader_DoWork(object sender, DoWorkEventArgs e)
        {
            MyFile file=(MyFile)e.Argument;
            using (FileStream output = new FileStream(file.Path, FileMode.Create))
            {
                SinchronizeFileProgressInfo.ProgressBytes = 0;
                SinchronizeFileProgressInfo.ProgressProcent = 0;
                SinchronizeFileProgressInfo.Action = FileStatus.Download;

                using (FileSystemClient serverFileSystem = new FileSystemClient())
                {
                    StreamWithProgress fileSourceStream = new StreamWithProgress(serverFileSystem.GetFileStream(selectedFileId, Account.GetUserEmail(), Account.GetUserPass()));
                    fileSourceStream.ProgressChanged += SetProgressInfoData;
                    fileSourceStream.CopyTo(output);
                }
            }
        }
        //--------------------------------------------------------------------------------


        //--------------------------------------------------------------------------------
        private void backgroundDownloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressForm.Dispose();
        }
        //--------------------------------------------------------------------------------


        //устанавливает прогресс синхронизации
        void SetProgressInfoData(object sender, StreamWithProgress.ProgressChangedEventArgs e)
        {
            SinchronizeFileProgressInfo.ProgressBytes = e.BytesRead; // количество обработанных байт
            SinchronizeFileProgressInfo.ProgressProcent = 0;
            if (e.BytesRead > 0)
            {
                float progressProcent = ((float)(e.BytesRead / 1024)) / ((float)(SinchronizeFileProgressInfo.File.Size / 1024));
                if (progressProcent < 100)
                    SinchronizeFileProgressInfo.ProgressProcent = (int)(progressProcent * 100); // процент обработки
            }
        }
        //--------------------------------------------------------------------------------


        
    }
}
