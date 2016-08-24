Imports Microsoft.VisualBasic
Imports Ada.Framework.Util.Number.TO
Imports Ada.Framework.Util.Core.Validation.TO
Imports Ada.Framework.Util.Core.Editions.TO
Imports System.Globalization

Namespace Ada.Framework.Util.Number
    ''' <summary>
    ''' Utilitario de números.
    ''' </summary>
    ''' <remarks>
    '''     Registro de versiones:
    '''
    '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
    ''' </remarks>
    Friend Class Numero : Implements INumero

        ''' <summary>
        ''' Valida y formatea un número según las condiciones especificadas.
        ''' </summary>
        ''' <remarks>
        '''     Registro de versiones:
        '''
        '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
        ''' </remarks>
        ''' <param name="numero">Número a formatear y editar.</param>
        ''' <param name="cantidadEnteros">Cantidad de enteros que permite el número.</param>
        ''' <param name="cantidadDecimales">Cantidad de decimales que permite el número.</param>
        ''' <param name="permiteCero">Indica si el número puede ser cero.</param>
        ''' <param name="permiteNegativos">Indica si el número puede ser negativo.</param>
        ''' <param name="separadorMiles">Separador de miles.</param>
        ''' <param name="separadorDecimales">Separador de decimales.</param>
        ''' <returns></returns>
        Public Function Validar(ByVal numero As String, ByVal cantidadEnteros As Integer, _
                                 ByVal cantidadDecimales As Integer, ByVal permiteCero As Boolean, _
                                 ByVal permiteNegativos As Boolean, ByVal separadorMiles As String, _
                                 ByVal separadorDecimales As String,
                                 Optional ByVal tratarVacioComoCero As Boolean = True) As ResultadoValidacion(Of Double, EstadoNumero) Implements INumero.Validar
            Dim retorno As ResultadoValidacion(Of Double, EstadoNumero) = New ResultadoValidacion(Of Double, EstadoNumero)()

            If Not (numero Is Nothing) Then
                numero = numero.Trim()
            End If

            If (String.IsNullOrEmpty(numero)) Then
                If tratarVacioComoCero Then
                    retorno.CodigoEstado = EstadoNumero.NUMERR_NOZERO
                Else
                    retorno.CodigoEstado = EstadoNumero.NUMERR_VACIO
                End If
                Return retorno
            End If

            Dim matriz(14, 9) As Integer
            Dim str_nume As String
            Dim str_digi As String
            Dim num_nent As Integer
            Dim num_ndec As Integer
            Dim num_stdo As Integer
            Dim ind_zero As Boolean
            Dim ind_sgno As Boolean
            Dim tken As Integer
            Dim i As Integer
            Dim j As Integer
            Dim k As Integer

            Dim tiene_decimales As Boolean
            Dim ind_sgno_pos As Boolean
            Dim ind_sgno_neg As Boolean
            Dim caracter As Char

            Call CargarMatriz(matriz)

            retorno.ValorAdicional = ""
            str_nume = Trim(numero)
            str_digi = ""
            tiene_decimales = False
            ind_zero = True
            ind_sgno = False
            ind_sgno_pos = False
            ind_sgno_neg = False
            num_nent = 0
            num_ndec = 0
            num_stdo = 1

            If Trim(str_nume) = "" Then
                str_nume = "0"
                ind_zero = True
            Else
                i = 1
                Do While i <= str_nume.Length And num_stdo <> 0
                    caracter = Trim(Mid(str_nume, i, 1))
                    Select Case caracter
                        Case "0"
                            str_digi = str_digi + caracter
                            tken = 2
                            If tiene_decimales Then
                                num_ndec = num_ndec + 1
                            Else
                                If num_nent > 0 Then
                                    num_nent = num_nent + 1
                                End If
                            End If
                        Case "1", "2", "3", "4", "5", "6", "7", "8", "9"
                            str_digi = str_digi + caracter
                            tken = 3
                            ind_zero = False
                            If tiene_decimales Then
                                num_ndec = num_ndec + 1
                            Else
                                num_nent = num_nent + 1
                            End If
                        Case separadorMiles
                            tken = 4
                        Case separadorDecimales
                            tken = 5
                            tiene_decimales = True
                        Case "+"
                            tken = 6
                            ind_sgno = True
                            ind_sgno_pos = True
                        Case "-"
                            tken = 7
                            ind_sgno = True
                            ind_sgno_neg = True
                        Case ""
                            tken = 8
                        Case Else
                            tken = 9
                    End Select
                    num_stdo = matriz(num_stdo, tken)
                    i = i + 1
                Loop
            End If
            If num_stdo = 0 Then
                retorno.CodigoEstado = EstadoNumero.NUMERR_INVAL
                retorno.ValorAdicional = ""
                Return retorno
            End If
            If matriz(num_stdo, 1) <> 1 Then
                retorno.CodigoEstado = EstadoNumero.NUMERR_INVAL
                retorno.ValorAdicional = ""
                Return retorno
            End If
            If ind_zero = True And permiteCero = False Then
                retorno.CodigoEstado = EstadoNumero.NUMERR_NOZERO
                retorno.ValorAdicional = ""
                Return retorno
            End If
            If tiene_decimales Then
                If cantidadDecimales = 0 Then
                    retorno.CodigoEstado = EstadoNumero.NUMERR_DECISIN
                    retorno.ValorAdicional = ""
                    Return retorno
                Else
                    If num_ndec > cantidadDecimales Then
                        retorno.CodigoEstado = EstadoNumero.NUMERR_DECIMAYOR
                        retorno.ValorAdicional = cantidadDecimales
                        Return retorno
                    End If
                End If
            End If
            If num_nent > cantidadEnteros Then
                retorno.CodigoEstado = EstadoNumero.NUMERR_ENTEMAYOR
                retorno.ValorAdicional = cantidadEnteros
                Return retorno
            End If
            If ind_sgno = True And permiteNegativos = False Then
                retorno.CodigoEstado = EstadoNumero.NUMERR_NOSIGO
                retorno.ValorAdicional = ""
                Return retorno
            End If
            '-----------------------------------------------
            ' Ahora corresponde devolver el valor del número
            '-----------------------------------------------
            If str_digi = "" Then
                str_digi = 0
            End If
            If num_ndec > 0 Then
                retorno.Valor = CDbl((str_digi) / (10 ^ num_ndec))
            Else
                retorno.Valor = CDbl(str_digi)
            End If
            If ind_sgno_neg = True Then
                retorno.Valor = retorno.Valor * -1
            End If
            '---------------------------------------------
            ' Ahora corresponde devolver el número editado
            '---------------------------------------------
            j = 0
            k = 0
            If cantidadDecimales > 0 Then
                If num_ndec < cantidadDecimales Then
                    Dim cero As String = ""
                    For x As Integer = 1 To cantidadDecimales - num_ndec
                        cero = cero + "0"
                    Next
                    str_digi = str_digi & cero
                End If
            End If

            retorno.ValorFormateado = ""
            For i = str_digi.Length To 1 Step -1
                caracter = Mid(str_digi, i, 1)
                If cantidadDecimales > 0 And j < cantidadDecimales Then
                    j = j + 1
                    If j = cantidadDecimales Then
                        retorno.ValorFormateado = separadorDecimales & caracter & retorno.ValorFormateado
                    Else
                        retorno.ValorFormateado = caracter & retorno.ValorFormateado
                    End If
                Else
                    k = k + 1
                    If k = 3 Then
                        If i > 1 Then
                            retorno.ValorFormateado = separadorMiles & caracter & retorno.ValorFormateado
                        Else
                            retorno.ValorFormateado = caracter & retorno.ValorFormateado
                        End If
                        k = 0
                    Else
                        retorno.ValorFormateado = caracter & retorno.ValorFormateado
                    End If
                End If
            Next i
            'If cantidadEnteros > 0 And num_nent = 0 Then
            'numeroFormateado = 0 & numeroFormateado
            'End If
            If ind_sgno_neg = True Then
                retorno.ValorFormateado = "-" & retorno.ValorFormateado
            End If

            Return retorno
        End Function

        ''' <summary>
        ''' Formatea un número según las condiciones especificadas.
        ''' </summary>
        ''' <remarks>
        '''     Registro de versiones:
        '''
        '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
        ''' </remarks>
        ''' <param name="numero">Número a editar.</param>
        ''' <param name="cantidadDecimales">Cantidad de decimales que permite el número. Si la cantidad es -1 el número no tiene limite.</param>
        ''' <param name="separadorMiles">Separador de miles.</param>
        ''' <param name="separadorDecimales">Separador de decimales.</param>
        ''' <param name="permiteAproximar">Indica si se debe aproximar los decimales si la cantidad de decimales requeridos es menor a la cantidad
        ''' que posee el numero. Parámetro Opcional.</param>
        ''' <returns></returns>
        Public Function Editar(ByVal numero As Double, ByVal cantidadDecimales As Integer, ByVal separadorMiles As String, _
                                ByVal separadorDecimales As String, Optional ByVal permiteAproximar As Boolean = False) As String Implements INumero.Editar
            Dim inicio As String = ChrW(1)

            Dim decimales As String = "##############################"

            If (cantidadDecimales <> -1) Then
                decimales = String.Empty
            End If

            For index = 1 To cantidadDecimales
                If (permiteAproximar) Then
                    decimales += "0"
                Else
                    decimales += "#"
                End If
            Next

            Dim salida As String = numero.ToString("#,##0." + decimales, CultureInfo.InvariantCulture)
            salida = salida _
            .Replace(",", inicio) _
            .Replace(".", separadorDecimales) _
            .Replace(inicio, separadorMiles)

            Return salida
        End Function

        ''' <summary>
        ''' Llena una matriz bidimensional para validar números según la teoría de Autómatas.
        ''' </summary>
        ''' <param name="matriz">Matriz bidimensional.</param>
        ''' <remarks>
        '''     Registro de versiones:
        '''
        '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
        ''' </remarks>
        Private Sub CargarMatriz(ByVal matriz(,) As Integer)
            Dim fila01 As String
            Dim fila02 As String
            Dim fila03 As String
            Dim fila04 As String
            Dim fila05 As String
            Dim fila06 As String
            Dim fila07 As String
            Dim fila08 As String
            Dim fila09 As String
            Dim fila10 As String
            Dim fila11 As String
            Dim fila12 As String
            Dim fila13 As String
            Dim fila14 As String
            Dim i As Integer

            fila01 = "010204001203030100"
            fila02 = "010204001200001400"
            fila03 = "000204001200000300"
            fila04 = "010505081200001400"
            fila05 = "010606081200001400"
            fila06 = "010707081200001400"
            fila07 = "010707001200001400"
            fila08 = "000909000000000000"
            fila09 = "001010000000000000"
            fila10 = "001111000000000000"
            fila11 = "010000081200001400"
            fila12 = "001313000000000000"
            fila13 = "011313000000001400"
            fila14 = "010000000000001400"

            For i = 1 To 9
                matriz(1, i) = Val(Mid(fila01, i * 2 - 1, 2))
                matriz(2, i) = Val(Mid(fila02, i * 2 - 1, 2))
                matriz(3, i) = Val(Mid(fila03, i * 2 - 1, 2))
                matriz(4, i) = Val(Mid(fila04, i * 2 - 1, 2))
                matriz(5, i) = Val(Mid(fila05, i * 2 - 1, 2))
                matriz(6, i) = Val(Mid(fila06, i * 2 - 1, 2))
                matriz(7, i) = Val(Mid(fila07, i * 2 - 1, 2))
                matriz(8, i) = Val(Mid(fila08, i * 2 - 1, 2))
                matriz(9, i) = Val(Mid(fila09, i * 2 - 1, 2))
                matriz(10, i) = Val(Mid(fila10, i * 2 - 1, 2))
                matriz(11, i) = Val(Mid(fila11, i * 2 - 1, 2))
                matriz(12, i) = Val(Mid(fila12, i * 2 - 1, 2))
                matriz(13, i) = Val(Mid(fila13, i * 2 - 1, 2))
                matriz(14, i) = Val(Mid(fila14, i * 2 - 1, 2))
            Next i

        End Sub
    End Class
End Namespace