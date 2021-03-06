﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Pikto.ViewModel;
using Pikto.View;

namespace Pikto.Command
{
	class OpenExaminationPathWizardCommand : ICommand
	{
		private IContentChange contentChange;

		public OpenExaminationPathWizardCommand(IContentChange contentChange)
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
			contentChange.SecondaryViewType = ViewType.ExaminationPathWizard;
		}
	}
}
