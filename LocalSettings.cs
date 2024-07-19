using System.ComponentModel;

public class LocalSettings : INotifyPropertyChanged
{
    private readonly AppConfig _config;
    private SettingsModel _settingsModel;

    public LocalSettings(AppConfig config)
    {
        _config = config;
    }

    public SettingsModel SettingsModel
    {
        get { return _settingsModel; }
        set
        {
            _settingsModel = value;
            OnPropertyChanged("SettingsModel");
        }
    }

    public bool IsPro
    {
        get
        {
            return _config.ForceProStatus || this.SettingsModel.IsPro;
        }
        set
        {
            if (!_config.ForceProStatus)
            {
                this.SettingsModel.IsPro = value;
            }
            this.OnPropertyChanged("IsPro");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
