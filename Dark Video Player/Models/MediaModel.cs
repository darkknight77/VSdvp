using System;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Dark_Video_Player
{
    public class MediaModel
{
    public MediaModel(StorageFile file)
    {
        MediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromStorageFile(file));
        
        
    }

    public string Title { get; set; }
    public Uri ArtUri { get; set; }
    public MediaPlaybackItem MediaPlaybackItem { get; private set; }
}
}