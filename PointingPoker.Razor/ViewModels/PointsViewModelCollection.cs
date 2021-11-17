namespace PointingPoker.Razor.ViewModels;

public class PointsViewModelCollection : List<PointsViewModel>
{
    public PointsViewModelCollection()
    {
        this.AddRange(new PointsViewModel[]
        {
            new PointsViewModel{ IsChecked = true, Value = 999, Display = "?"},
            new PointsViewModel{ IsChecked = true, Value = 9999, Display = "0 points"},
            new PointsViewModel{ IsChecked = true, Value = 1, Display = "1 point"},
            new PointsViewModel{ IsChecked = true, Value = 2, Display = "2 points"},
            new PointsViewModel{ IsChecked = true, Value = 3, Display = "3 points"},
            new PointsViewModel{ IsChecked = false, Value = 4, Display = "4 points"},
            new PointsViewModel{ IsChecked = true, Value = 5, Display = "5 points"},
            new PointsViewModel{ IsChecked = false, Value = 6, Display = "6 points"},
            new PointsViewModel{ IsChecked = false, Value = 7, Display = "7 points"},
            new PointsViewModel{ IsChecked = true, Value = 8, Display = "8 points"},
            new PointsViewModel{ IsChecked = false, Value = 9, Display = "9 points"},
            new PointsViewModel{ IsChecked = true, Value = 10, Display = "10 points"},
            new PointsViewModel{ IsChecked = true, Value = 13, Display = "13 points"},
            new PointsViewModel{ IsChecked = true, Value = 20, Display = "20 points"},
            new PointsViewModel{ IsChecked = false, Value = 40, Display = "40 points"},
            new PointsViewModel{ IsChecked = false, Value = 100, Display = "100 points"},
        });
    }
}
