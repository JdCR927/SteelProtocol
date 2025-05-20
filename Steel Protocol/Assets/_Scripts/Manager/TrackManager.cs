using UnityEngine;
using SteelProtocol.Data.Track;

namespace SteelProtocol.Manager
{
    public class TrackManager : MonoBehaviour
    {
        private TrackData[] tracks;

        public void LoadData()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("JSON/tracks");
            if (jsonFile == null)
            {
                Debug.LogError("tracks.json not found in Resources/JSON/");
                return;
            }

            string wrappedJson = "{\"tracks\":" + jsonFile.text + "}";
            TrackDataList dataList = JsonUtility.FromJson<TrackDataList>(wrappedJson);
            tracks = dataList.tracks;
        }

        public TrackData GetTrackById(string id)
        {
            foreach (var track in tracks)
            {
                if (track.id == id)
                    return track;
            }

            Debug.LogWarning($"Track ID '{id}' not found.");
            return null;
        }
    }
}