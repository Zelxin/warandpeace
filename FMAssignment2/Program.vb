Imports System
Imports System.IO
Imports System.Text.RegularExpressions

Module Program
    Sub Main(args As String())
        Dim fileData = File.ReadAllLines("warandpeace.txt")

        Top10MostFrequentWords(fileData)



        Console.ReadKey()
    End Sub

    Private Function Top10MostFrequentWords(data As IEnumerable(Of String)) As List(Of KeyValuePair(Of String, Integer))
        Dim wordCount = New Dictionary(Of String, Integer)

        For Each line In data
            'Remember that the split function by default will include empty entries.
            'Which will cause the rest of this function issues if you don't remember to deal with them
            'You can also just use the string split options to remove them!
            Dim words = line.Split(" "c, StringSplitOptions.RemoveEmptyEntries)
            For Each word In words
                'Changed this to do the replace on each word instead, but it was removing my spaces.
                Dim tempWord = Regex.Replace(word, "[^A-Za-z]+", String.Empty)
                If wordCount.ContainsKey(tempWord) Then
                    wordCount(tempWord) += 1
                Else
                    wordCount.Add(tempWord, 1)
                End If
            Next
        Next

        'If you know linq you can do it like this
        Dim top10Words = wordCount.OrderByDescending(Function(x) x.Value).Take(10).ToList()

        'Dim top10Words As New List(Of KeyValuePair(Of String, Integer))
        'For Each pair In wordCount
        '    If top10Words.Count = 0 Then
        '        top10Words.Add(pair)
        '        Continue For
        '    End If
        '    'This compairs the value of the current pair in the loop
        '    'To the mininmum value in our current top 10 list.
        '    If pair.Value > top10Words.Min(Function(x) x.Value) Then
        '        top10Words.Add(pair)
        '    End If
        'Next

        top10Words = top10Words.OrderByDescending(Function(x) x.Value).ToList().Take(10).ToList()
        Return top10Words
    End Function
End Module
