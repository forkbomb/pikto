﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pikto.ViewModel.SimpleViewModel;

namespace Pikto.View.ViewManager.ViewSimpleManager
{
	class ViewTypeLearningPathPromptManager : ViewTypeSimpleManager<LearningPathWindow, LearningPathViewModel>
	{
		protected override LearningPathWindow CreateView()
		{
			var view = new LearningPathWindow();
			view.DataContext = ViewModel;
			return view;
		}

		protected override LearningPathViewModel CreateViewModel()
		{
			var viewModel = new LearningPathViewModel();
			return viewModel;
		}
	}
}