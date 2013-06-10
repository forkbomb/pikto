﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Pikto.Utils;
using Pikto.Command;

namespace Pikto.ViewModel.WizardViewModel
{
	class PictogramsManagementPathNavigationViewModel : WizardNavigationViewModel<PictogramsManagementPathViewModel>
	{
        ICommand finishCmd;
		public PictogramsManagementPathNavigationViewModel(PictogramsManagementPathViewModel viewModel, Action<string> refreshStepAction, ICommand cancelCmd, ICommand finishedCmd)
			: base(viewModel, refreshStepAction, cancelCmd)
		{
            WizardTitle = "Zarządzanie piktogramami";
            finishCmd = finishedCmd;
		}

        public string WizardTitle { get; set; }

		protected override void PrepareForwardCmds(IDictionary<string, ICommand> commands)
		{
			commands.Add("", new BasicCommand(p =>
			{
				switch (ViewModel.Action)
				{
					case ChooseEnum.New:
					{
						NextStep("new_picto");
                        ViewModel.HandleCategories();
                        ViewModel.PreparePictogram();
                        ViewModel.SetAsNew();
						break;
					}
					case ChooseEnum.Existing:
					{
						NextStep("update_picto");
                        ViewModel.SetAsOld();
						break;
					}
				}

                ViewModel.HandleCategories();
			}));

			commands.Add("new_picto", new BasicCommand(p =>
			{
                if (ViewModel.ChosenCategory==null || ViewModel.PictoName==null)
                {
                    System.Windows.MessageBox.Show("Wypełnij pola.", "Błąd", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                else
                {
                    ViewModel.PutName();
                    ViewModel.PutCategory();
                    ViewModel.HandleCamera();
                    NextStep("picto_camera");
                }
                
			}));

            commands.Add("picto_camera", new BasicCommand(p =>
            {
                ViewModel.StopHandlingCamera();
                NextStep("picto_medium");
            }));

            commands.Add("picto_medium", new BasicCommand(p =>
            {
                if (ViewModel.MediaType == null || ViewModel.ObjectName == null)
                {
                    System.Windows.MessageBox.Show("Wypełnij pola.", "Błąd", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                else
                {
                    ViewModel.PutObject();
                    if (ViewModel.NewPictogram)
                    {
                        ViewModel.AddPiktogram();
                        System.Windows.MessageBox.Show("Dodawanie piktogramu zakończone powodzeniem.", "Gratulacje", System.Windows.MessageBoxButton.OK);
                    }
                    else
                    {
                        ViewModel.EditPictogram();
                        System.Windows.MessageBox.Show("Edycja piktogramu zakończona powodzeniem.", "Gratulacje", System.Windows.MessageBoxButton.OK);
                    }
                    finishCmd.Execute(null);
                }
                
            }));

			commands.Add("update_picto", new BasicCommand(p =>
			{
                var param = ViewModel.ChosenPictogram;
                if (param == null)
                {
                    System.Windows.MessageBox.Show("Wybierz piktogram.", "Błąd", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                else
                {
                    NextStep("edit_picto");
                }
			}));

            commands.Add("edit_picto", new BasicCommand(p =>
            {
                switch (ViewModel.EditAction)
                {
                    case ActionEnum.Update:
                        {
                            ViewModel.PreparePictogramToEdit();
                            NextStep("new_picto");
                            break;
                        }
                    case ActionEnum.Delete:
                        {
                            ViewModel.DeletePictogram();
                            System.Windows.MessageBox.Show("Usunięcie piktogramu zakończone powodzeniem.", "Gratulacje", System.Windows.MessageBoxButton.OK);
                            finishCmd.Execute(null);
                            break;
                        }
                }

            }));

           
		}
	}
}
