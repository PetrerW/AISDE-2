clear all;
close all;

fid = fopen('E:\Piotr\Programowanie\C#\AISDE2\Stare\Lab2Console\Lab2Console\PlotBuffSize.txt'); % otwiera plik
[A kom] = fscanf(fid, '%g ' ); % wczytuje do macierzy A
if(A<0)
  disp(kom);  
end

fclose(fid);

time = fopen('E:\Piotr\Programowanie\C#\AISDE2\Stare\Lab2Console\Lab2Console\PlotDataTime.txt'); % otwiera plik
[t com] = fscanf(time, '%g ' ); % wczytuje do macierzy A
if(t<0)
  disp(com);  
end

fclose(time);

velocity = fopen('E:\Piotr\Programowanie\C#\AISDE2\Stare\Lab2Console\Lab2Console\PlotStreamVelocity.txt'); % otwiera plik
[v com] = fscanf(velocity, '%g' ); % wczytuje do macierzy A
if(v<0)
  disp(com);  
end

fclose(velocity);

current = fopen('E:\Piotr\Programowanie\C#\AISDE2\Stare\Lab2Console\Lab2Console\current.txt'); % otwiera plik
[ctime tom] = fscanf(current, '%g' ); % wczytuje do macierzy A
if(ctime<0)
  disp(com);  
end

fclose(current);

vp = fopen('E:\Piotr\Programowanie\C#\AISDE2\Stare\Lab2Console\Lab2Console\vpList.txt'); % otwiera plik
[vp_ ziom] = fscanf(vp, '%g' ); % wczytuje do macierzy A
if(vp_<0)
  disp(ziom);  
end

fclose(vp);
%  for i=1:length(t)
% newtime(i)=sum(t(1:2:i));
% if(rem(i,2)~=0)
%      newtime(i)=sum(t(1:2:i));
% else
%     
%      newtime(i)=newtime(i-1);
% end
%  end
 cnewtime=ctime;
 velocity=v(1:2:length(v));
 
 if(length(A)>length(velocity))
A=A(1:length(velocity));
 
 end
 
 plot(cnewtime(1:2:length(cnewtime)),A);
figure (2);
plot(cnewtime(1:(length(cnewtime)-1)),v(2:length(v)));
figure(3);
plot(cnewtime(1:(length(cnewtime)-1)),vp_(2:length(vp_)));
