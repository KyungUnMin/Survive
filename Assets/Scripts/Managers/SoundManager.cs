using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
                                    //<경로,오디오클립>
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    private AudioSource[] audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    
    //사운드 오브젝트 최상위 부모 생성
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));//enum -> string배열
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        //모든 오디오 정지
        foreach (AudioSource audioSource in audioSources)
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

    private void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f, float volumn = 1.0f)//Play이너함수
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

        //BGM일땐 굳이 Dictionary찾지도 않음, 바로 폴더에서 꺼내오기
        if (type == Define.Sound.Bgm)
        {
            audioClip = GameManager.Resource.Load<AudioClip>(path);
        }
        else//Effect일땐
        {
            //Dictionary에 없을때는 Load후 Dictionary에 저장하기(추후 또 쓸때를 대비)
            if (audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = GameManager.Resource.Load<AudioClip>(path);
                audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"이 오디오소스 경로 잘못됨 :  {path}");

        return audioClip;
    }

    

}
