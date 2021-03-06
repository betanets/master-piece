﻿using System;
using System.Windows.Forms;

namespace master_piece
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoadingForm.ShowSplashScreen();
            MainForm mainForm = new MainForm();
            LoadingForm.CloseForm();

            Application.Run(mainForm);
        }
    }
}
