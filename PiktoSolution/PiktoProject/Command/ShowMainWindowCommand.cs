﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Pikto.ViewModel;
using Pikto.View;

namespace Pikto.Command
{
	class ShowMainWindowCommand : ICommand
	{
		private IContentChange contentChange;

		public ShowMainWindowCommand(IContentChange contentChange)
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
			contentChange.PrimaryViewType = ViewType.MainWindow;
			contentChange.SecondaryViewType = ViewType.Default;
		}
	}
}
