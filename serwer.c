
//Example code: A simple server side code, which echos back the received message. 
//Handle multiple socket connections with select and fd_set on Linux  
#include <stdio.h>  
#include <string.h>   //strlen  
#include <stdlib.h>  
#include <errno.h>  
#include <unistd.h>   //close  
#include <arpa/inet.h>    //close  
#include <sys/types.h>  
#include <sys/socket.h>  
#include <netinet/in.h>  
#include <sys/time.h> //FD_SET, FD_ISSET, FD_ZERO macros  
     
#define TRUE   1  
#define FALSE  0  
#define PORT 8888  
     
void myMemcpy(char *dest, char *source)
{	//własny memCpy
	int len = strlen(source);
	int i = 0;
	for(i = 0; i < len; i++)
	{
		*(dest + i) = *(source + i);
	}
}

int main(int argc , char *argv[])   
{   
    int opt = TRUE;   
    int master_socket , addrlen , new_socket , client_socket[30] ,  
          max_clients = 16 , activity, i , valread , sd;   
    int max_sd;   
    struct sockaddr_in address;   
         
    char buffer[1024];  //data buffer of 1K  
         
    //set of socket descriptors  
    fd_set readfds;   
         
    //a message  
    char *message = "ECHO Daemon v1.0 \r\n\0";   
    
    //informacje o grze
    char nicknames[16][1024];	//pseudonimy wszystkich graczy
    char serverNames[16][1024];	//nazwy serwerów
    char gamesStat[16];		//stany serwerów 0-wyłączony, 1-w trakcie, 2-zastopowany, 3-oczekuje
    char gameMoves[16][9];	//aktualne ruchy graczy
    char whosMove[16];		//czyj jest ruch na serwerze
    char sign[16];		//posiadany znak przez gracza X czy O
    int onServer[16]; 		//na, któ©ym serwerze przebywa gracz: -1 na żadnym serwerze
    

     
    //inicjalizacja socketów i informacji o serwerach
    for (i = 0; i < max_clients; i++)   
    {   
        client_socket[i] = 0;   
	gamesStat[i] = 0;
	onServer[i] = -1;
    }   
         
    //tworzenie master_socketa  
    if( (master_socket = socket(AF_INET , SOCK_STREAM , 0)) == 0)   
    {   
        perror("blad socketa");   
        exit(EXIT_FAILURE);   
    }   
     
    //ustawianie master_socketa na mozliwosc obslugi wielu polaczen
    if( setsockopt(master_socket, SOL_SOCKET, SO_REUSEADDR, (char *)&opt,  
          sizeof(opt)) < 0 )   
    {   
        perror("setsockopt");   
        exit(EXIT_FAILURE);   
    }   
     
    //opis socketa, rodzaj adresow (ipv4) i dodanie portu (tutaj 8888)  
    address.sin_family = AF_INET;   
    address.sin_addr.s_addr = INADDR_ANY;   
    address.sin_port = htons( PORT );   
         
    //przypisanie socketa do adresu komputer (localhost) i portu 8888  
    if (bind(master_socket, (struct sockaddr *)&address, sizeof(address))<0)   
    {   
        perror("przypisanie nie powiodlo sie");   
        exit(EXIT_FAILURE);   
    }   
    printf("Nasluchuje na porcie: %d \n", PORT);   
         
    //maksymalnie 3 przychodzace polaczenia do master_socket
    if (listen(master_socket, 3) < 0)   
    {   
        perror("listen");   
        exit(EXIT_FAILURE);   
    }   
         
    //akceptowanie polaczen przychodzacych 
    addrlen = sizeof(address);   
    puts("Oczekiwanie na polaczenia...");   
         
    while(TRUE)   
    {   
        //pusty socket  
        FD_ZERO(&readfds);   
     
        //ustawianie info z master_socketa  
        FD_SET(master_socket, &readfds);   
        max_sd = master_socket;   
             
        //obsluga socketow potomnych  
        for ( i = 0 ; i < max_clients ; i++)   
        {   
            //socket descriptor  
            sd = client_socket[i];   
                 
            //jezeli jest to prawidlowy sd to dodaj go do czytajacych deskryptorow 
            if(sd > 0)   
                FD_SET( sd , &readfds);   
                 
            //najwyzszy sd - potrzebny pozniej
            if(sd > max_sd)   
                max_sd = sd;   
        }   
     
        //oczekiwanie na aktywnosc ktoregos sd, timeout ustawiony na NULL wiec czeka w nieskonczonosc
        activity = select( max_sd + 1 , &readfds , NULL , NULL , NULL);   
       
        if ((activity < 0) && (errno!=EINTR))   
        {   
            printf("select error");   
        }   
             
        //Jezeli jest aktywnosc na master_sockecie to jest to przychodzace polaczenie
        if (FD_ISSET(master_socket, &readfds))   
        {   
            if ((new_socket = accept(master_socket, (struct sockaddr *)&address, (socklen_t*)&addrlen))<0)   
            {   
                perror("accept");   
                exit(EXIT_FAILURE);   
            }   
             
            //informacje o numerze socketa  
            printf("Nowe polaczenie, socket fd %d , ip: %s , port: %d\n" , new_socket , inet_ntoa(address.sin_addr) , ntohs(address.sin_port));   
           
            //Wysylanie pierwszej wiadomosci  
            if( send(new_socket, message, strlen(message), 0) != strlen(message) )   
            {   
                printf("send");   
            }    
                 
            //dodawanie socketa do tablicy socketow
            for (i = 0; i < max_clients; i++)   
            {   
                //dodaj do pierwszej pusej pozycji
                if( client_socket[i] == 0 )   
                {   
                    client_socket[i] = new_socket;   
                    printf("Dodawanie do tablicy socketow jako: %d\n" , i);   
                         
                    break;   
                }   
            }   
        }   
             
        //operacje wej/wyj na socketach 
        for (i = 0; i < max_clients; i++)   
        {   
            sd = client_socket[i];   
            if (FD_ISSET( sd , &readfds))   
            {   
                //Sprawdz czy socket sie rozlacza i odbierz wiadomosc
                if ((valread = read( sd , buffer, 1023)) == 0)   
                {   
                    //Socket rozlaczyl sie  
                    getpeername(sd , (struct sockaddr*)&address , (socklen_t*)&addrlen);   
                    printf("Klient rozlaczony, ip: %s , port: %d\n", inet_ntoa(address.sin_addr) , ntohs(address.sin_port));   
                         
                    //Zamknij socket i dodaj do listy by uzyc go ponownie  
                    close(sd);   
                    client_socket[i] = 0;   
                }   
                     
                //Czytaj dane 
                else 
                {   
                    buffer[valread] = '\0';
		    printf("%c-%d\n", buffer[0], i);

		    if(buffer[0] == 'c')
		    {	//połącz z serwerem
			memcpy(nicknames[i], &buffer[1], strlen(&buffer[1] + 1));
			onServer[i] = -1;
			int jed = i % 10;
			int dzi = (i - jed) / 10;
			buffer[0] = dzi + 48;
			buffer[1] = jed + 48;
			send(sd, buffer, 2, 0);
		    }
		    if(buffer[0] == 'r')
		    {	//odswież listę serwerów
			int j = 0;
			int actCurs = 0;
			for(j = 0; j < max_clients; j++)
			{
			    if(gamesStat[j] == '3')
			    {
				int jed = j % 10;
				int dzi = (j - jed) / 10;
				buffer[actCurs] = dzi + 48;
				buffer[actCurs + 1] = jed + 48;
				actCurs += 2;
				memcpy(&buffer[actCurs], serverNames[j], strlen(serverNames[j]));
				actCurs += strlen(serverNames[j]);
				buffer[actCurs] = '\n';
				actCurs++;
                            }
			}
			if(actCurs == 0)
			{
			    buffer[0] = 0;
			    actCurs = 1;				
			}
			send(sd, buffer, actCurs, 0);
		    }
		    if(buffer[0] == 'm')
		    {	//załóż serwer
			//printf("%s %d\n", &buffer[1], strlen(&buffer[1]) );
			myMemcpy(serverNames[i], &buffer[1]);
			memcpy(&(serverNames[i][0]), &buffer[1], strlen(&buffer[1]) + 1);
			onServer[i] = i;
			gamesStat[i] = '3';
			int j = 0;
			for(j = 0; j < 9; j++)
		 	    gameMoves[i][j] = ' ';
			whosMove[onServer[i]] = 'X';
			buffer[0] = '1';
			buffer[1] = 0;
			send(sd, buffer, 1, 0);
		    }
		    if(buffer[0] == 'j')
		    {	//dołącz do serwera
			int liczba = (buffer[1] - 48) * 10 + (buffer[2] - 48);
			if(gamesStat[liczba] == '3')
			{
			    buffer[0] = '1';
			    memcpy(&buffer[1], nicknames[liczba], strlen(nicknames[liczba]));
			    send(sd, buffer, strlen(buffer), 0);
			    gamesStat[liczba] = '1';
			    onServer[i] = liczba;
			}
			else
			{
			    buffer[0] = '0';
			    buffer[1] = 0;
			    send(sd, buffer, 1, 0);
			}
		    }
		    if(buffer[0] == 'g')
		    {	//odśwież grę
			buffer[0] = gamesStat[onServer[i]];
			int j = 0;
			for(j = 0; j < 9; j++)
		 	    buffer[j + 1] = gameMoves[onServer[i]][j];
			buffer[10] = whosMove[onServer[i]];
			//nazwa przeciwnika zaleznie od znaku
			printf("stan gry to: %c\n", buffer[0]);
			if(buffer[0] == '1' || buffer[0] == '2')
			{
			    if(i == onServer[i])
			    {
				for(j = 0; j < max_clients; j++)
				{
				    if(i == j)
					continue;
				    if(onServer[j] == i)
				    {
					memcpy(&buffer[11], nicknames[j], strlen(nicknames[j]));
					buffer[11 + strlen(nicknames[j])] = 0;
					break;
				    }
				}
			    }
			    else
			    {
				memcpy(&buffer[11], nicknames[onServer[i]], strlen(nicknames[onServer[i]]));
				buffer[11 + strlen(nicknames[onServer[i]])] = 0;
			    }
			}
			else
			{
			    buffer[11] = 'd';
			    buffer[12] = 0;
			}
			send(sd, buffer, strlen(buffer), 0);
		    }
		    if(buffer[0] == 'h')
		    {	//opuść grę
			if(onServer[i] != -1)
			{
				gamesStat[onServer[i]] = '0';	
				//onServer[onServer[i]] = -1;
			}
			onServer[i] = -1;
			send(sd, buffer, 1, 0);
			
		    }
		    if(buffer[0] == 'd')
		    {	//opuść serwer
			send(sd, buffer, 1, 0);
		    }
		    if(buffer[0] == 'q')
		    {	//wykonaj ruch
			int j = 0;
			for(j = 0; j < 9; j++)
			{
			    gameMoves[onServer[i]][j] = buffer[j + 1];
			}
			whosMove[onServer[i]] = buffer[10];
			send(sd, buffer, 1, 0);
		    }
		    if(buffer[0] == 'w')
		    {	//zatrzymaj gre po wygranej
			gamesStat[onServer[i]] = '2';
			send(sd, buffer, 1, 0);
		    }
		    if(buffer[0] == 'u')
		    {	//wznow gre
			gamesStat[onServer[i]] = '1';
			send(sd, buffer, 1, 0);
		    }
		    //czysc poczatek bufora, z ktorego czytane i wysylane sa dane
		    buffer[0] = 0;
                }   
            }   
        }   
    }   
         
    return 0;   
}   

