Imports System.IO
Imports System.Text.RegularExpressions
'Vincent Vassallo 
'I noticed that I didnt' originally make this project a .netframework console app
'Here it is as .net framework. the only difference i noteiced was the splitting a string has to be slightly different
'for .net framework
Module Module1

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

            'This is a weird thing in .net framework where it won't let me specify the 
            Dim words = line.Split({" "}, StringSplitOptions.RemoveEmptyEntries)
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
        'Order by descending, does just that, the parameter it takes is a
        'Lambda (or inline) function that tells which parameter to use for the orderby
        'Take(10) is a function that returns the first 10 items from an ienumerable, and toList()
        'turns the data back into a list to match our functions return type.
        Dim top10WordsLinq = wordCount.OrderByDescending(Function(x) x.Value).Take(10).ToList()
        Return top10WordsLinq
    End Function


End Module
