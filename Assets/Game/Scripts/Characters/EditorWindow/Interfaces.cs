using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CharactersEditor
{
    public interface ISelect
    {
        bool isSelected { get; set; }
    }

    public interface IRename
    {
        bool isRenaming { get; }
        void RenameStart();
        void RenameCancel();
        void RenameAccept();
    }

    public interface IDuplicate
    {
        void Duplicate();
    }

    public interface ICategory
    {
        string[] categories { get; }
    }
}