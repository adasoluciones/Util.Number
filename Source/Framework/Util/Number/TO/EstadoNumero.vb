
Namespace Ada.Framework.Util.Number.TO
    ''' <summary>
    ''' Representa el estado de la validación o edicion de un número.
    ''' </summary>
    ''' <remarks>
    '''     Registro de versiones:
    '''
    '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
    ''' </remarks>
    Public Enum EstadoNumero
        ''' <summary>
        ''' Valor que indica que el número es válido.
        ''' </summary>
        ''' <remarks>
        '''     Registro de versiones:
        '''
        '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
        ''' </remarks>
        Valido = 0
        ''' <summary>
        ''' Valor que indica que el número es inválido o el valor no es un número.
        ''' </summary>
        ''' <remarks>
        '''     Registro de versiones:
        '''
        '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
        ''' </remarks>
        NUMERR_INVAL = 1
        ''' <summary>
        ''' Valor que indica que el número no puede ser cero.
        ''' </summary>
        ''' <remarks>
        '''     Registro de versiones:
        '''
        '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
        ''' </remarks>
        NUMERR_NOZERO = 2
        ''' <summary>
        ''' Valor que indica que el número no permite decimales.
        ''' </summary>
        ''' <remarks>
        '''     Registro de versiones:
        '''
        '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
        ''' </remarks>
        NUMERR_DECISIN = 3
        ''' <summary>
        ''' Valor que indica que el número de decimales es mayor al permitido. Entonces el número de decimales permitidos
        ''' será el valor adicional.
        ''' </summary>
        ''' <remarks>
        '''     Registro de versiones:
        '''
        '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
        ''' </remarks>
        NUMERR_DECIMAYOR = 4
        ''' <summary>
        ''' Valor que indica que el número de enteros es mayor al permitido. Entonces el número de enteros permitidos
        ''' será el valor adicional.
        ''' </summary>
        ''' <remarks>
        '''     Registro de versiones:
        '''
        '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
        ''' </remarks>
        NUMERR_ENTEMAYOR = 5
        ''' <summary>
        ''' Valor que indica que el número no permite signos.
        ''' </summary>
        ''' <remarks>
        '''     Registro de versiones:
        '''
        '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
        ''' </remarks>
        NUMERR_NOSIGO = 6
        ''' <summary>
        ''' Valor que indica que el texto está vacío.
        ''' </summary>
        ''' <remarks>
        '''     Registro de versiones:
        '''
        '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
        ''' </remarks>
        NUMERR_VACIO = 7
    End Enum
End Namespace
