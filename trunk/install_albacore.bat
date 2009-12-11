@ECHO OFF
@ECHO Adding gemcutter.org as gem source
call gem source -a http://gemcutter.org
@ECHO Installing albacore gem
call gem install albacore
pause