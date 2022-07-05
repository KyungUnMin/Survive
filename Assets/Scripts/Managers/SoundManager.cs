using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private AudioSource[] audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();//<경로,오디오클립>
    
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);//씬에서 사운드를 총괄하는 오브젝트 (없다면)생성

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));//sound enum값들을 string배열로 변경
            for (int i = 0; i < soundNames.Length - 1; i++)//enum값-1 만큼 AudiioSource컴포넌트 부착한 오브젝트 생성
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;//사운드 총괄 오브젝트의 자식으로 넣음
            }

            audioSources[(int)Define.Sound.Bgm].loop = true;//BGM은 Loop켜기
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in audioSources)//모든 오디오 정지
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        audioClips.Clear();
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f, float volumn = 1.0f)//Play래핑함수
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);//path를 통해 오디오클립 받아옴
        Play(audioClip, type, pitch, volumn);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f, float volumn = 1.0f)//Play이너함수
    {
        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm)//BGM
        {
            AudioSource audioSource = audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.volume = volumn;
            audioSource.Play();
        }
        else//Effect
        {
            AudioSource audioSource = audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.volume = volumn;
            audioSource.PlayOneShot(audioClip);//한번만 재생
        }
    }

    //사운드를 사용할때마다 일일히 Load하지 않고 Dictionary에 호출하는 용도의 함수(딕셔너리에 없을때만 Load)
    private AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)//BGM일땐 굳이 Dictionary찾지도 않음
        {
            audioClip = GameManager.Resource.Load<AudioClip>(path);//바로 폴더에서 꺼내오기
        }
        else//Effect일땐
        {
            if (audioClips.TryGetValue(path, out audioClip) == false)//Dictionary에 없을때는 Load후 Dictionary에 저장하기(추후 또 쓸때를 대비)
            {
                audioClip = GameManager.Resource.Load<AudioClip>(path);
                audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"이 오디오소스 경로 잘못됨 ㅅㄱ :  {path}");

        return audioClip;
    }

    public AudioSource AddAudio(GameObject target, string clipPath)
    {
        AudioSource audio;
        audio = target.GetOrAddComponent<AudioSource>();
        AudioClip clip = GameManager.Resource.Load<AudioClip>("Sounds/" + clipPath);
        if (clip == null)
            Debug.LogError(clipPath + "의 경로를 찾을수 없음");

        audio.clip = clip;
        audio.loop = true;
        audio.playOnAwake = false;
        return audio;
    }

}
