[System.Serializable]
public class Question 
{
    public string question; // Pertanyaan
    public string[] answers; // Opsi jawaban
    public int correctAnswerIndex; // Indeks jawaban yang benar
    public int timeToAnswer; // Waktu untuk menjawab pertanyaan (ditambahkan)
}
