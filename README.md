# jwt-token-dll
Библиотека для управления JWT токенами

## Создание приватного и публичного ключа
<code>openssl req -x509 -sha1 -out test.crt -new -nodes -keyout test.key -newkey rsa:2048 -set_serial 0</code>

## Добавление .dll после сборки в папку со всеми .dll

<code>
  <Target Name="PostBuild" AfterTargets="PostBuildEvent">
    <Exec Command="copy /Y bin\Release\net6.0\JwtToken.dll YOUR_PATH" />
  </Target>
</code>
