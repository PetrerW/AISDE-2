clear all;
close all;

fid = fopen('E:\Piotr\Programowanie\C#\AISDE2\Lab2Console\Lab2Console\PlotBuffSize.txt'); % otwiera plik
[A kom] = fscanf(fid, '%g ' ); % wczytuje do macierzy A
if(A<0)
  disp(kom);  
end

fclose(fid);

time = fopen('E:\Piotr\Programowanie\C#\AISDE2\Lab2Console\Lab2Console\PlotDataTime.txt'); % otwiera plik
[t com] = fscanf(time, '%g ' ); % wczytuje do macierzy A
if(t<0)
  disp(com);  
end

fclose(time);

velocity = fopen('E:\Piotr\Programowanie\C#\AISDE2\Lab2Console\Lab2Console\PlotStreamVelocity.txt'); % otwiera plik
[v com] = fscanf(velocity, '%g' ); % wczytuje do macierzy A
if(v<0)
  disp(com);  
end

fclose(velocity);
 for i=1:length(t)
newtime(i)=sum(t(1:2:i));
if(rem(i,2)~=0)
     newtime(i)=sum(t(1:2:i));
else
    
     newtime(i)=newtime(i-1);
end
 end
 

if length(A)>length(v)/2
    A = A(1:length(v)/2);
end

plot(newtime(1:2:length(newtime)),A);
figure (2);
plot(newtime(1:(length(newtime)-1)),v(2:length(v)));
