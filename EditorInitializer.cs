#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

////////////////////////////////////////////////////////////////////////////////
//   External dependency :  IEditorInitialized interface                      //
//                                                                            //
//   Handles Unity3D editor to initialize our game at editor time             //  
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
[InitializeOnLoad]
public class EditorInitializer
{
  ////////////////////////////////////////////////////////////////////////////////
  //                                                                            //
  //    statically called when scripts are loaded                               //
  //                                                                            //  
  ////////////////////////////////////////////////////////////////////////////////
  static EditorInitializer()
  {
    EditorApplication.update += RunOnce; // non-statically call RunOnce from editor Update()
  }

  ////////////////////////////////////////////////////////////////////////////////
  //    EditorInitialize() is called when Edit / Editor Initialize button is    //
  //    clicked at ThrowCustomException Unity Editor bar.                       //
  //                                                                            //
  //    Finds every IEditorInitialized implemented ( either active or inactive )//
  //    int our Game, and calls The EditorInitialize() method                   //
  //                                                                            //
  //                                                                            //  
  ////////////////////////////////////////////////////////////////////////////////
  [MenuItem ("Edit/Editor Initialize")] //places a button at editor EDIT / RunOnce .
  static public void EditorInitialize () 
  {
    List<IEditorInitialized> editorInitializeList = UnityUtils.SlowFindAllComponents<IEditorInitialized>();
    foreach(  IEditorInitialized editorInitialize in editorInitializeList )
    {
      editorInitialize.EditorInitialize();
    }
  }

  ////////////////////////////////////////////////////////////////////////////////
  //                                                                            //
  //                                                                            //
  //                                                                            //  
  ////////////////////////////////////////////////////////////////////////////////
  static void RunOnce()
  {
    //EditorInitialize(); // do it if you want EditorInitialize() to be called automatically everytime the scripts are loaded. 
    EditorApplication.update -= RunOnce; // unsubscribe to be called just once.
  }
}
#endif