﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Pikto.View;
using Pikto.ViewModel;

namespace Pikto.Command
{
	class StartExaminationPathCommand : ICommand
	{
		private IContentChange contentChange;
		private IParameterInput<string> parameterInput;

		public StartExaminationPathCommand(IContentChange contentChange, IParameterInput<string> parameterInput)
		{
			this.contentChange = contentChange;
			this.parameterInput = parameterInput;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			parameterInput.Parameter = parameter as string;
			contentChange.PrimaryViewType = ViewType.ExaminationPath;
		}
	}
}
