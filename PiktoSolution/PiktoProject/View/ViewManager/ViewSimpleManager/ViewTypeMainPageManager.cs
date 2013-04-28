﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pikto.ViewModel;
using System.Windows.Input;

namespace Pikto.View.ViewManager.ViewSimpleManager
{
	class ViewTypeMainPageManager : ViewTypeSimpleManager<MainPage, MainPageViewModel>
	{
		private ICommand startLearningPathCmd;
		private ICommand startExaminationPathCmd;
		private ICommand settingsCmd;
		private ICommand aboutCmd;
		private ICommand exitCmd;

		public ViewTypeMainPageManager(ICommand startLearningPathCmd, ICommand startExaminationPathCmd, ICommand settingsCmd, ICommand aboutCmd, ICommand exitCmd)
		{
			this.startLearningPathCmd = startLearningPathCmd;
			this.startExaminationPathCmd = startExaminationPathCmd;
			this.settingsCmd = settingsCmd;
			this.aboutCmd = aboutCmd;
			this.exitCmd = exitCmd;
		}

		protected override MainPage CreateView()
		{
			var mainPage = new MainPage();
			mainPage.DataContext = ViewModel;
			return mainPage;
		}

		protected override MainPageViewModel CreateViewModel()
		{
			var viewModel = new MainPageViewModel(startLearningPathCmd, startExaminationPathCmd, settingsCmd, aboutCmd, exitCmd);
			return viewModel;
		}
	}
}
