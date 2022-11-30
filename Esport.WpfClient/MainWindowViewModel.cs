using Esport.Data;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Esport.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        #region
        public RestCollection<Team> Teams { get; set; }

        public ICommand CreateTeamCommand { get; set; }
        public ICommand UpdateTeamCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }

        private Team selectedTeam;

        public Team SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                if (value != null)
                {
                    selectedTeam = new Team()
                    {
                        Name = value.Name,
                        Odd = value.Odd,
                        BettedAmount = value.BettedAmount,
                        ID = value.ID,
                        Match = value.Match,
                        MatchID = value.MatchID,
                        Wins = value.Wins,
                    };
                    OnPropertyChanged();
                    (DeleteTeamCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        #endregion

        #region
        public RestCollection<Match> Matches { get; set; }

        public ICommand CreateMatchCommand { get; set; }
        public ICommand UpdateMatchCommand { get; set; }
        public ICommand DeleteMatchCommand { get; set; }

        private Match selectedMatch;

        public Match SelectedMatch
        {
            get { return selectedMatch; }
            set
            {
                if (value != null)
                {
                    selectedMatch = new Match()
                    {
                        Name = value.Name,
                        ID = value.ID,
                        Location = value.Location,
                        Team1ID = value.Team1ID,
                        Team2ID = value.Team2ID,
                    };
                    OnPropertyChanged();
                    (DeleteMatchCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        #endregion

        #region
        public RestCollection<Location> Locations { get; set; }

        public ICommand CreateLocationCommand { get; set; }
        public ICommand UpdateLocationCommand { get; set; }
        public ICommand DeleteLocationCommand { get; set; }

        private Location selectedLocation;

        public Location SelectedLocation
        {
            get { return selectedLocation; }
            set
            {
                if (value != null)
                {
                    selectedLocation = new Location()
                    {
                        Name = value.Name,
                        Capacity = value.Capacity,
                        ID = value.ID,
                        Match = value.Match,
                        MatchID = value.MatchID,
                    };
                    OnPropertyChanged();
                    (DeleteLocationCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateLocationCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        #endregion
        public static bool IsIndesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public MainWindowViewModel()
        {
            if (!IsIndesignMode)
            {
                
                Teams = new RestCollection<Team>("https://localhost:44332/", "team", "hub");
                CreateTeamCommand = new RelayCommand(() =>
                {
                    Teams.Add(new Team()
                    {
                        Name = SelectedTeam.Name,
                        ID = SelectedTeam.ID,
                        Wins = SelectedTeam.Wins,
                        BettedAmount = SelectedTeam.BettedAmount,
                        Odd = SelectedTeam.Odd
                    });
                });
                UpdateTeamCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Teams.Update(SelectedTeam);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });
                DeleteTeamCommand = new RelayCommand(() =>
                {
                    Teams.Delete(SelectedTeam.ID);
                }, () => { return SelectedTeam != null; });
                SelectedTeam = new Team();
                Locations = new RestCollection<Location>("https://localhost:44332/", "location", "hub");

                CreateLocationCommand = new RelayCommand(() =>
                {
                    Locations.Add(new Location()
                    {
                        Name = SelectedLocation.Name,
                        Capacity = SelectedLocation.Capacity,
                        ID = SelectedLocation.ID,
                        Match = SelectedLocation.Match,
                        MatchID = SelectedLocation.MatchID
                    });
                });

                DeleteLocationCommand = new RelayCommand(() =>
                {
                    Locations.Delete(SelectedLocation.ID);
                },
                () =>
                {
                    return SelectedLocation != null;
                });
                
                UpdateLocationCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Locations.Update(SelectedLocation);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });
                SelectedLocation = new Location();
                

                Matches = new RestCollection<Match>("https://localhost:44332/", "match", "hub");

                CreateMatchCommand = new RelayCommand(() =>
                {
                    Matches.Add(new Match()
                    {
                        Name = SelectedMatch.Name,
                        ID = SelectedMatch.ID,
                        Location = SelectedMatch.Location,
                        Team1ID = SelectedMatch.Team1ID,
                        Team2ID = SelectedMatch.Team2ID
                    });
                });

                DeleteMatchCommand = new RelayCommand(() =>
                {
                    Matches.Delete(SelectedMatch.ID);
                },
                () =>
                {
                    return SelectedMatch != null;
                });

                UpdateMatchCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Matches.Update(SelectedMatch);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });
                SelectedMatch = new Match();
            }

        }

    }
}