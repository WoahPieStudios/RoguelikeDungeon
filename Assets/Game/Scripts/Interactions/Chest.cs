using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Items;
using Game.Parties;
using Game.Animations;

namespace Game.Interactions
{
    public class Chest : Interactable, IOpenClose
    {
        [Header("Drop Item")]
        [SerializeField]
        DropItem _DropItemPrefab;

        [Header("Animation")]
        [SerializeField]
        AnimationData _OpenAnimData;
        [SerializeField]
        AnimationData _CloseAnimData;

        [Header("Sound Effects")]
        [SerializeField]
        AudioClip _OpenSFX;
        [SerializeField]
        AudioClip _CloseSFX;

        AnimationHandler _AnimationHandler;
        AudioSource _AudioSource;

        protected bool _CanInteract = true;

        public override bool canInteract => _CanInteract;

        public IItem[] items { get; set; }

        public bool isOpen => !_CanInteract;

        protected virtual void Awake() 
        {
            _AnimationHandler = GetComponent<AnimationHandler>();

            _AudioSource = GetComponent<AudioSource>();

            // _AnimationHandler.AddAnimationData(_OpenAnimData);
            // _AnimationHandler.AddAnimationData(_CloseAnimData);
        }

        public void Open()
        {
            _CanInteract = false;

            for(int i = 0; i < items.Length; i++)
            {
                DropItem dropItem = Instantiate(_DropItemPrefab, transform.position, Quaternion.identity);

                dropItem.item = items[i];
                dropItem.OnDrop();
            }

            // _AnimationHandler.Play(_OpenAnimData);

            // _AudioSource.PlayOneShot(_OpenSFX);
        }

        public void Close()
        {
            _CanInteract = true;

            // _AnimationHandler.Play(_CloseAnimData);

            // _AudioSource.PlayOneShot(_CloseSFX);
        }

        public override void OnInteract(Party party)
        {
            if(isOpen && (items == null || items.Length <= 0))
                return;

            _CanInteract = false;

            Open();

            // for(int i = 0; i < items.Length; i++)
            //     items[i].Use(party);
        }
    }
}