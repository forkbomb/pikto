﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Pikto.ViewModel;
using Pikto.View;

namespace Pikto.Command
{
	class ShowAboutCommand : ICommand
	{
		private IContentChange contentChange;

		public ShowAboutCommand(IContentChange contentChange)
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
			contentChange.SecondaryViewType = ViewType.AboutWindow;
		}
	}
}
