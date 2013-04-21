﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Pikto.Views;

namespace Pikto.ViewModel.Commands
{
	class StartExaminationPathCommand : ICommand
	{
		private IContentChange contentChange;

		public StartExaminationPathCommand(IContentChange contentChange)
		{
			this.contentChange = contentChange;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			contentChange.SecondaryViewType = ViewType.StartExaminationPathWizard;
		}
	}
}
