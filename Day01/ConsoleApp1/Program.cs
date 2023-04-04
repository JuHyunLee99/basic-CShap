using System;   // System이라는 namespace 사용

// 네임스페이스 ConsolaApp1
namespace ConsoleApp1   // 프로젝트 명이랑 같음, 클래스 모음
{
    /// <summary>
    /// 프로그램 클래스
    /// </summary>
    internal class Program  //internal = private랑 비슷    // 파일명과 클래스명을 동일하게 쓰는게 관리에 편함
    {
        /// <summary>
        /// 메인 엔트리 포인트
        /// </summary>
        /// <param name="args">콘솔 매개변수</param>
        static void Main(string[] args) // static(정적) 메소드
        {
            Console.WriteLine("Hello C#");
        }
    }
}
