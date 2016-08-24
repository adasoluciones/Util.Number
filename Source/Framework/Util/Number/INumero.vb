Imports Ada.Framework.Util.Core.Editions.TO
Imports Ada.Framework.Util.Number.TO
Imports Ada.Framework.Util.Core.Validation.TO

Namespace Ada.Framework.Util.Number
    ''' <summary>
    ''' Contrato del utilitario de números.
    ''' </summary>
    ''' <remarks>
    '''     Registro de versiones:
    '''
    '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
    ''' </remarks>
    Public Interface INumero
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
        ''' <param name="tratarVacioComoCero">Indica si debe tratar el valor vacio como cero.</param>
        Function Validar(ByVal numero As String, ByVal cantidadEnteros As Integer, _
                                 ByVal cantidadDecimales As Integer, ByVal permiteCero As Boolean, _
                                 ByVal permiteNegativos As Boolean, ByVal separadorMiles As String, _
                                 ByVal separadorDecimales As String,
                                 Optional ByVal tratarVacioComoCero As Boolean = True) As ResultadoValidacion(Of Double, EstadoNumero)

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
        ''' que posee el numero.</param>
        Function Editar(ByVal numero As Double, ByVal cantidadDecimales As Integer, ByVal separadorMiles As String, _
                                ByVal separadorDecimales As String, Optional ByVal permiteAproximar As Boolean = False) As String
    End Interface

End Namespace
