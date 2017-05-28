Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Diagnostics

Namespace ViewModel
    ''' <summary>
    ''' Base class for all ViewModel classes in the application.
    ''' It provides support for property change notifications 
    ''' and has a DisplayName property.  This class is abstract.
    ''' </summary>
    Public MustInherit Class ViewModelBase
        Implements INotifyPropertyChanged, IDisposable

#Region "Constructor"

        Protected Sub New()
        End Sub

#End Region ' Constructor

#Region "DisplayName"

        ''' <summary>
        ''' Returns the user-friendly name of this object.
        ''' Child classes can set this property to a new value,
        ''' or override it to determine the value on-demand.
        ''' </summary>
        Private privateDisplayName As String
        Public Overridable Property DisplayName() As String
            Get
                Return privateDisplayName
            End Get
            Protected Set(ByVal value As String)
                privateDisplayName = value
            End Set
        End Property

#End Region ' DisplayName

#Region "Debugging Aides"

        ''' <summary>
        ''' Warns the developer if this object does not have
        ''' a public property with the specified name. This 
        ''' method does not exist in a Release build.
        ''' </summary>
        <Conditional("DEBUG"), DebuggerStepThrough()> _
        Public Sub VerifyPropertyName(ByVal propertyName As String)
            ' Verify that the property name matches a real,  
            ' public, instance property on this object.
            If TypeDescriptor.GetProperties(Me)(propertyName) Is Nothing Then
                Dim msg As String = "Invalid property name: " & propertyName

                If Me.ThrowOnInvalidPropertyName Then
                    Throw New Exception(msg)
                Else
                    Debug.Fail(msg)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Returns whether an exception is thrown, or if a Debug.Fail() is used
        ''' when an invalid property name is passed to the VerifyPropertyName method.
        ''' The default value is false, but subclasses used by unit tests might 
        ''' override this property's getter to return true.
        ''' </summary>
        Private privateThrowOnInvalidPropertyName As Boolean
        Protected Overridable Property ThrowOnInvalidPropertyName() As Boolean
            Get
                Return privateThrowOnInvalidPropertyName
            End Get
            Set(ByVal value As Boolean)
                privateThrowOnInvalidPropertyName = value
            End Set
        End Property

#End Region ' Debugging Aides

#Region "INotifyPropertyChanged Members"

        ''' <summary>
        ''' Raised when a property on this object has a new value.
        ''' </summary>
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        ''' <summary>
        ''' Raises this object's PropertyChanged event.
        ''' </summary>
        ''' <param name="propertyName">The property that has a new value.</param>
        Protected Overridable Sub OnPropertyChanged(ByVal propertyName As String)
            Me.VerifyPropertyName(propertyName)

            Dim handler As PropertyChangedEventHandler = Me.PropertyChangedEvent
            If handler IsNot Nothing Then
                Dim e = New PropertyChangedEventArgs(propertyName)
                handler(Me, e)
            End If
        End Sub

#End Region ' INotifyPropertyChanged Members

#Region "IDisposable Support"
        Private disposedValue As Boolean ' �d������Ăяo�������o����ɂ�

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: �}�l�[�W��Ԃ�j�����܂� (�}�l�[�W �I�u�W�F�N�g)�B
                End If

                ' TODO: �A���}�l�[�W ���\�[�X (�A���}�l�[�W �I�u�W�F�N�g) ��������A���� Finalize() ���I�[�o�[���C�h���܂��B
                ' TODO: �傫�ȃt�B�[���h�� null �ɐݒ肵�܂��B
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: ��� Dispose(ByVal disposing As Boolean) �ɃA���}�l�[�W ���\�[�X���������R�[�h������ꍇ�ɂ̂݁AFinalize() ���I�[�o�[���C�h���܂��B
        'Protected Overrides Sub Finalize()
        '    ' ���̃R�[�h��ύX���Ȃ��ł��������B�N���[���A�b�v �R�[�h����� Dispose(ByVal disposing As Boolean) �ɋL�q���܂��B
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' ���̃R�[�h�́A�j���\�ȃp�^�[���𐳂��������ł���悤�� Visual Basic �ɂ���Ēǉ�����܂����B
        Public Sub Dispose() Implements IDisposable.Dispose
            ' ���̃R�[�h��ύX���Ȃ��ł��������B�N���[���A�b�v �R�[�h����� Dispose(ByVal disposing As Boolean) �ɋL�q���܂��B
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace