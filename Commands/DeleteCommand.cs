﻿using MusicStoreDB_App.ViewModels;
using System;
using System.Windows.Input;

namespace MusicStoreDB_App.Commands {
    public class DeleteCommand : ICommand {
        private readonly dynamic baseViewModel;

        public DeleteCommand(BaseViewModel baseViewModel) {
            this.baseViewModel = baseViewModel;
        }

        public event EventHandler CanExecuteChanged {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            switch (baseViewModel.Name) {
                case "Композиции":
                    baseViewModel.DeleteSongData();
                    break;
                case "Альбомы":
                    baseViewModel.DeleteAlbumData();
                    break;
                case "Продажи":
                    baseViewModel.DeletePurchaseData();
                    break;
                case "Композиции по альбомам":
                    baseViewModel.DeleteAlbumSongData();
                    break;
            }
        }
    }
}