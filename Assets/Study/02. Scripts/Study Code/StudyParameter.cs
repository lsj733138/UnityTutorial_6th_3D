using System;
using UnityEngine;

public class StudyParameter : MonoBehaviour
{
   public int number = 10;

   private void Start()
   {
      NormalParameter(number);
   }

   private void NormalParameter(int num)
   {
      Debug.Log("호출 전 : {number}");
      number = 100;

      Debug.Log("호출 후 : {number}");
   }

   private void RefParameter(ref int num)
   {
      Debug.Log("호출 전 : {number}");
      number = 100;

      Debug.Log("호출 후 : {number}");
   }

   private void OutParameter(out int num)
   {
      Debug.Log("호출 전 : {number}");
      num = 100;

      Debug.Log("호출 후 : {number}");
   }
}
