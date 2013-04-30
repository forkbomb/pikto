﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Pikto.ViewModel.SimpleViewModel;
using Pikto.Utils;
using Pikto.View;
using Pikto.View.ViewManager;
using Pikto.View.ViewManager.ViewSimpleManager;
using Pikto.View.ViewManager.ViewWizardManager;
using Pikto.ViewModel;

namespace Pikto
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			BuildApplication();
		}

		private void BuildApplication()
		{
			var appViewModel = new AppViewModel();
			var parameterTransfer = new ParameterTransfer();

			var contentManagementService = new ContentManagementService(appViewModel, parameterTransfer);

			var mapping = PrepareMapping(contentManagementService, parameterTransfer);
			var viewTypeConverter = new ViewTypeToUIElementConverter(mapping);
			Application.Current.Resources.Add("viewTypeConverter", viewTypeConverter);

			var mainView = new AppView();
			mainView.DataContext = appViewModel;

			appViewModel.PrimaryViewType = ViewType.MainPage;
			appViewModel.SecondaryViewType = ViewType.Default;

			mainView.Show();
		}

		private IDictionary<ViewType, ViewTypeManager<UIElement>> PrepareMapping(ContentManagementService cms, IParameterTransfer parameterTransfer)
		{
			var mapping = new Dictionary<ViewType, ViewTypeManager<UIElement>>();

			mapping.Add(ViewType.Default, new ViewTypeDefaultManager());
			mapping.Add(ViewType.Test, new ViewTypeTestManager());

			mapping.Add(ViewType.MainPage, new ViewTypeMainPageManager(cms.MenuStartLearningPathCommand, cms.MenuExaminationPathWizardCommand, cms.ShowSettingsPageCommand, cms.AboutWindowCommand, cms.CloseApplicationCommand));
			
			mapping.Add(ViewType.StartLearningPath, new ViewTypeStartLearningPathManager(cms.StartLearningPathCommand));
			mapping.Add(ViewType.ExaminationPathWizard, new ViewTypeExaminationPathWizardManager(vt => { cms.RefreshSecondaryView(ViewType.ExaminationPathWizard, vt); }, cms.HideSecondaryWindowCommand, cms.StartExaminationPathCommand));
            
			mapping.Add(ViewType.LearningPath, new ViewTypeLearningPathManager());
			mapping.Add(ViewType.ExaminationPath, new ViewTypeExaminationPathManager(parameterTransfer));

			mapping.Add(ViewType.SettingsPage, new ViewTypeSettingsPageManager(cms.ShowStartPiktogramsManagementPathWizardCommand, cms.ShowStartCategoriesManagementPathWizardCommand, cms.ShowStartCameraCalibrationWizardCommand, cms.LoadMainPageCommand));
            mapping.Add(ViewType.StartPiktogramsManagementWizard, new ViewTypePiktogramsManagementWizardManager(vt => { cms.RefreshSecondaryView(ViewType.StartPiktogramsManagementWizard, vt); }, cms.HideSecondaryWindowCommand));
            mapping.Add(ViewType.StartCategoriesManagementWizard, new ViewTypeCategoriesManagementWizardManager(vt => { cms.RefreshSecondaryView(ViewType.StartCategoriesManagementWizard, vt); }, cms.HideSecondaryWindowCommand));
			mapping.Add(ViewType.AboutWindow, new ViewTypeAboutManager(cms.HideSecondaryWindowCommand));

			return mapping;
		}
	}
}
