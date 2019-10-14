﻿using MusicStoreDB_App.Commands;
using MusicStoreDB_App.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MusicStoreDB_App.ViewModels {
    public class SongViewModel : BaseViewModel {
        public CollectionViewSource ListSongs { get; private set; }       
        private Song selectedItem;
        public Song SelectedItem {
            get { return selectedItem; }
            set {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
                ButtonAddContent = "Добавить";
            }
        }
        public SongViewModel() {
            ListSongs = new CollectionViewSource();
            RefreshData();
            SelectedItem = ListSongs.View.CurrentItem as Song;
            ButtonAddContent = "Добавить";
            SaveEvent = new SaveCommand(this);
            AddEvent = new AddCommand(this);
            RefreshEvent = new RefreshCommand(this);
            DeleteEvent = new DeleteCommand(this);
        }
        public void RefreshData() {
            using (var dbContext = new MusicStoreDBEntities()) {
                ListSongs.Source = dbContext.Songs.ToList();
            }
        }
        public void SaveChanges() {
            try {
                using (var dbContext = new MusicStoreDBEntities()) {
                    if (ButtonAddContent == "Отмена") {
                        AddSongData(dbContext);
                        ButtonAddContent = "Добавить";
                    } else {
                        EditSongData(dbContext);
                    }
                }
                RefreshData();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        public void AddSongData(MusicStoreDBEntities dbContext) {
            dbContext.Songs.Add(SelectedItem);
            dbContext.SaveChanges();
        }
        public void EditSongData(MusicStoreDBEntities dbContext) {
            dbContext.Entry(SelectedItem).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
        public void DeleteSongData() {
            try {
                using (var dbContext = new MusicStoreDBEntities()) {
                    var entity = ListSongs.View.CurrentItem as Song;
                    dbContext.Songs.Remove(entity);
                    dbContext.SaveChanges();
                    RefreshData();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}