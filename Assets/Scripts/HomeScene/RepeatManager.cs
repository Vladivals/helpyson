using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState
{
	Idle,
	Listening,
	Talking
}

[RequireComponent(typeof(AudioSource))]
public class RepeatManager : MonoBehaviour
{
	public Goal goal;

	private Animator _playerAnimator;
	private PlayerState _playerState = PlayerState.Idle;
	private AudioSource _audioSource;
	private string microphoneName;

	private float[] _clipSampleData;

	void Start()
	{
		string[] microphoneDevices = Microphone.devices;
		microphoneName = microphoneDevices[0];

		var playerGameObject = GameObject.FindGameObjectWithTag(ConstantsRepeatManager.PlayerTag);
		if (playerGameObject != null)
		{
			_playerAnimator = playerGameObject.GetComponent<Animator>();
		}

		_audioSource = GetComponent<AudioSource>();
		_clipSampleData = new float[ConstantsRepeatManager.SampleDataLength]/*1024*/;
		Idle();
	}

	private void Update()
	{

		if (_playerState == PlayerState.Idle && IsVolumeAboveThresold())
		{
			SwitchState();
		}
	}

	private bool IsVolumeAboveThresold()
	{
		if (_audioSource.clip == null)
		{
			return false;
		}

		_audioSource.clip.GetData(_clipSampleData, _audioSource.timeSamples); //Read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
		var clipLoudness = 0f;
		foreach (var sample in _clipSampleData)
		{
			clipLoudness += Mathf.Abs(sample);
		}
		clipLoudness /= ConstantsRepeatManager.SampleDataLength;

		Debug.Log("Clip Loudness = " + clipLoudness);

		return clipLoudness > ConstantsRepeatManager.SoundThreshold;
	}


	private void SwitchState()
	{
		switch (_playerState)
		{
			case PlayerState.Idle:
				_playerState = PlayerState.Listening;
				Listen();
				break;

			case PlayerState.Listening:
				_playerState = PlayerState.Talking;
				Talk();
				break; 

			case PlayerState.Talking:
				_playerState = PlayerState.Idle;
				Idle();
				break;
		}
	}


	private void Idle()
	{
		

		if (_playerAnimator != null)
		{
			_playerAnimator.SetTrigger(ConstantsRepeatManager.MecanimIdle);

			if (_audioSource.clip != null)
			{
				_audioSource.Stop();
				_audioSource.clip = null;
			}
			_audioSource.clip = Microphone.Start(microphoneName, true, ConstantsRepeatManager.IdleRecordingLength, ConstantsRepeatManager.RecordingFrequency);
		}
	}


	private void Listen()
	{
		if (_playerAnimator != null)
		{
			_playerAnimator.SetTrigger(ConstantsRepeatManager.MecanimListen);
			_audioSource.clip = Microphone.Start(microphoneName, false, ConstantsRepeatManager.RecordingLength, ConstantsRepeatManager.RecordingFrequency);
			Invoke("SwitchState", ConstantsRepeatManager.RecordingLength);
		}
	}


	private void Talk()
	{
		Debug.Log("talk");
		if (_playerAnimator != null)
		{
			_playerAnimator.SetTrigger(ConstantsRepeatManager.MecanimTalk);

			Microphone.End(null);
			if (_audioSource.clip != null)
			{
				_audioSource.Play();
			}

			Invoke("SwitchState", ConstantsRepeatManager.RecordingLength);
			Debug.Log("Talk talk");
			goal.OnSubgoalComplete();
		}
	}
}