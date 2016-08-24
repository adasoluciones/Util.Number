Namespace Ada.Framework.Util.Number
    ''' <summary>
    ''' Factoría del Utilitario de números.
    ''' </summary>
    ''' <remarks>
    '''     Registro de versiones:
    '''
    '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
    ''' </remarks>
    Public Class NumeroFactory
        ''' <summary>
        ''' Obtiene una implementación de INumero.
        ''' </summary>
        ''' <remarks>
        '''     Registro de versiones:
        '''
        '''         1.0 05/12/2012 Nicolas Fuentes M. (ADA Ltda.): Versión Inicial
        ''' </remarks>
        ''' <returns>Implementación de INumero.</returns>
        Public Shared Function ObtenerNumero() As INumero
            Return New Numero
        End Function
    End Class

End Namespace