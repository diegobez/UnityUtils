using UnityEngine;
using System.Collections;

////////////////////////////////////////////////////////////////////////////////
//   Interface intended to be implemented by game components that             //
//   want to be called from editor for initialization.                        //
//   Called from Initializer.cs                                               //
//                                                                            //  
////////////////////////////////////////////////////////////////////////////////
public interface IEditorInitialized
{
  ////////////////////////////////////////////////////////////////////////////////
  //  Called from the editor for game initialization                            //
  //  consider using #if UNITY_EDITOR wrapping the method code                  //
  //                                                                            //  
  ////////////////////////////////////////////////////////////////////////////////
  void EditorInitialize();
}

