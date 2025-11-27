#define NOMINMAX 

#include <iostream>
#include <vector> 
#include <fstream> 
#include <sstream>
#include <algorithm> 
#include <list> 
#include <cstdlib>
#include <ctime>
#include <conio.h>
#include <windows.h> 
#include <chrono> 
#include <iomanip> 
#include <cmath> 

using namespace std;



const int ANCHO_TABLERO = 20;
const int ALTO_TABLERO = 20;
const int LONGITUD_INICIAL = 5;
const int LONGITUD_LIMITE_NIVEL = LONGITUD_INICIAL + 10;
const string RANKING_FILE = "snake_ranking.txt";
const string SAVE_FILE = "snake_save.dat";
const int VELOCIDAD_BASE_MS = 150;
const int PTS_PREMIO = 100;
const int PTS_TRAMPA = -200;
const int PTS_NIVEL = 1000;



enum ConsolaColor {
    NEGRO = 0, AZUL = 1, VERDE = 2, CYAN = 3, ROJO = 4, MAGENTA = 5,
    AMARILLO = 6, BLANCO = 7, GRIS = 8, AZUL_CLARO = 9, VERDE_CLARO = 10,
    CYAN_CLARO = 11, ROJO_CLARO = 12, MAGENTA_CLARO = 13, AMARILLO_CLARO = 14,
    BLANCO_BRILLANTE = 15
};

void setColor(int color) {
    SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), color);
}

void gotoxy(int x, int y) {
    COORD coord;
    coord.X = x;
    coord.Y = y;
    SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), coord);
}

void SleepMs(int ms) {
    Sleep(ms);
}

#define CLEAR_SCREEN system("cls")



enum Direccion { PARAR = 0, ARRIBA, IZQUIERDA, ABAJO, DERECHA };

struct Coordenada {
    int x, y;
    bool operator==(const Coordenada& otro) const {
        return x == otro.x && y == otro.y;
    }
};

struct Record {
    string nombre;
    int puntuacion;
};



struct GameState {
    int puntuacion;
    int nivel;
    int dir;
    std::vector<Coordenada> snake_coords;
    Coordenada premio;
};




class SnakeGame {
private:
    const int CONSOLA_ANCHO_MAX = 80;
    const int CONSOLA_ALTO_MAX = 40;
    const int OFFSET_X = (CONSOLA_ANCHO_MAX / 2) - (ANCHO_TABLERO / 2);
    const int OFFSET_Y = (CONSOLA_ALTO_MAX / 2) - (ALTO_TABLERO / 2);

    std::list<Coordenada> snake;

    Coordenada premio;
    std::vector<Coordenada> trampas;

    Direccion dir;
    string nombre_jugador;
    int puntuacion;
    int nivel;
    int velocidad_ms;

    std::chrono::high_resolution_clock::time_point tiempo_inicio;
    double tiempo_final;
    bool partida_cargada;

 

    void inicializarSerpiente() {
        int start_x = ANCHO_TABLERO / 2;
        int start_y = ALTO_TABLERO / 2;
        for (int i = 0; i < LONGITUD_INICIAL; ++i) {
            snake.push_back({ start_x, start_y + i });
        }
    }

    void calcularVelocidad() {
        int nueva_velocidad = VELOCIDAD_BASE_MS - (nivel * 10);
        velocidad_ms = std::max(30, nueva_velocidad);
    }

    Coordenada generarElemento(const std::vector<Coordenada>& excluidas) {
        Coordenada pos;
        bool libre;
        do {
            libre = true;
            pos.x = 1 + rand() % (ANCHO_TABLERO - 2);
            pos.y = 1 + rand() % (ALTO_TABLERO - 2);

            for (const auto& elemento : excluidas) {
                if (elemento == pos) {
                    libre = false;
                    break;
                }
            }
        } while (!libre);
        return pos;
    }

    void generarAmbosElementos() {
        if (premio.x != 0) {
            gotoxy(premio.x + OFFSET_X, premio.y + OFFSET_Y);
            cout << " ";
        }
        for (const auto& trampa_anterior : trampas) {
            gotoxy(trampa_anterior.x + OFFSET_X, trampa_anterior.y + OFFSET_Y);
            cout << " ";
        }
        trampas.clear();

        std::vector<Coordenada> excluidas;
        excluidas.reserve(snake.size() + nivel + 1);

        for (const auto& segment : snake) {
            excluidas.push_back(segment);
        }

        if (premio.x == 0) {
            premio = generarElemento(excluidas);
        }
        excluidas.push_back(premio);

        gotoxy(premio.x + OFFSET_X, premio.y + OFFSET_Y);
        setColor(AMARILLO_CLARO);
        cout << "@";

        for (int i = 0; i < nivel; ++i) {
            Coordenada nueva_trampa = generarElemento(excluidas);
            trampas.push_back(nueva_trampa);
            excluidas.push_back(nueva_trampa);

            gotoxy(nueva_trampa.x + OFFSET_X, nueva_trampa.y + OFFSET_Y);
            setColor(ROJO_CLARO);
            cout << "X";
        }

        setColor(BLANCO);
    }

    void dibujarSerpiente() {
        if (snake.empty()) return;

        setColor(AZUL_CLARO);
        gotoxy(snake.front().x + OFFSET_X, snake.front().y + OFFSET_Y);
        cout << "O";

        setColor(AZUL);
        auto it = snake.begin();
        it++;
        while (it != snake.end()) {
            gotoxy(it->x + OFFSET_X, it->y + OFFSET_Y);
            cout << "o";
            it++;
        }
        setColor(BLANCO);
    }

    
    void dibujarHUD() {
        setColor(BLANCO_BRILLANTE);

      
        gotoxy(OFFSET_X, OFFSET_Y - 2);
        
        cout << "Pts: " << puntuacion << "    ";

      
        gotoxy(OFFSET_X + 10, OFFSET_Y - 2);
        cout << "Velocidad: " << (VELOCIDAD_BASE_MS - velocidad_ms) / 10 + 1 << "  ";

        
        gotoxy(OFFSET_X + 28, OFFSET_Y - 2);
        cout << "Nivel: " << nivel << "  ";

        
        gotoxy(OFFSET_X + 40, OFFSET_Y - 2);
        cout << "Longitud: " << snake.size() << "  ";

        
        gotoxy(OFFSET_X, ALTO_TABLERO + OFFSET_Y + 3);
        cout << "                                                    ";

        
        gotoxy(OFFSET_X, ALTO_TABLERO + OFFSET_Y + 1);
        cout << "Controles: W A S D | 'G' Guardar/Salir | 'X' Salir";

        setColor(BLANCO);
    }

    void dibujarFronteras() {
        CLEAR_SCREEN;
        setColor(VERDE_CLARO);
        for (int i = 0; i < ALTO_TABLERO; i++) {
            for (int j = 0; j < ANCHO_TABLERO; j++) {
                gotoxy(j + OFFSET_X, i + OFFSET_Y);
                if (i == 0 || i == ALTO_TABLERO - 1 || j == 0 || j == ANCHO_TABLERO - 1) {
                    cout << "#";
                }
                else {
                    cout << " ";
                }
            }
        }
        dibujarHUD();
        setColor(BLANCO);
    }

    void input() {
        if (_kbhit()) {
            char key = _getch();
            switch (key) {
            case 'w': case 'W': if (dir != ABAJO) dir = ARRIBA; break;
            case 'a': case 'A': if (dir != DERECHA) dir = IZQUIERDA; break;
            case 's': case 'S': if (dir != ARRIBA) dir = ABAJO; break;
            case 'd': case 'D': if (dir != IZQUIERDA) dir = DERECHA; break;
            case 'g': case 'G':
                dir = PARAR;
                guardarJuego();
                break;
            case 'x': case 'X': dir = PARAR;
            }
        }
    }

   

    void guardarJuego() {
        GameState estado;
        estado.puntuacion = puntuacion;
        estado.nivel = nivel;
        estado.dir = dir;
        estado.premio = premio;

        estado.snake_coords.assign(snake.begin(), snake.end());

        std::ofstream archivo(SAVE_FILE, std::ios::binary | std::ios::trunc);
        if (archivo.is_open()) {
            archivo.write((char*)&estado.puntuacion, sizeof(int));
            archivo.write((char*)&estado.nivel, sizeof(int));
            archivo.write((char*)&estado.dir, sizeof(int));
            archivo.write((char*)&estado.premio, sizeof(Coordenada));

            int size = estado.snake_coords.size();
            archivo.write((char*)&size, sizeof(int));
            archivo.write((char*)estado.snake_coords.data(), size * sizeof(Coordenada));

            archivo.close();

            
            gotoxy(OFFSET_X, ALTO_TABLERO + OFFSET_Y + 3);
            setColor(CYAN_CLARO);
            cout << "Partida guardada. Volviendo al menu...";
            SleepMs(1500);

        }
        else {
            
            gotoxy(OFFSET_X, ALTO_TABLERO + OFFSET_Y + 3);
            setColor(ROJO_CLARO);
            cout << "ERROR: No se pudo guardar la partida.";
            SleepMs(1500);
        }
    }

    bool cargarJuego() {
        std::ifstream archivo(SAVE_FILE, std::ios::binary);
        if (!archivo.is_open()) {
            return false;
        }

        GameState estado;
        int size;

        archivo.read((char*)&estado.puntuacion, sizeof(int));
        archivo.read((char*)&estado.nivel, sizeof(int));
        archivo.read((char*)&estado.dir, sizeof(int));
        archivo.read((char*)&estado.premio, sizeof(Coordenada));

        archivo.read((char*)&size, sizeof(int));
        estado.snake_coords.resize(size);
        archivo.read((char*)estado.snake_coords.data(), size * sizeof(Coordenada));

        archivo.close();

        puntuacion = estado.puntuacion;
        nivel = estado.nivel;
        dir = (Direccion)estado.dir;
        premio = estado.premio;

        snake.clear();
        snake.assign(estado.snake_coords.begin(), estado.snake_coords.end());
        calcularVelocidad();
        partida_cargada = true;

        return true;
    }

    void moverSerpiente() {
        if (dir == PARAR) return;

        Coordenada nueva_cabeza = snake.front();
        switch (dir) {
        case ARRIBA: nueva_cabeza.y--; break;
        case IZQUIERDA: nueva_cabeza.x--; break;
        case ABAJO: nueva_cabeza.y++; break;
        case DERECHA: nueva_cabeza.x++; break;
        default: break;
        }

        if (nueva_cabeza.x <= 0) nueva_cabeza.x = ANCHO_TABLERO - 2;
        else if (nueva_cabeza.x >= ANCHO_TABLERO - 1) nueva_cabeza.x = 1;

        if (nueva_cabeza.y <= 0) nueva_cabeza.y = ALTO_TABLERO - 2;
        else if (nueva_cabeza.y >= ALTO_TABLERO - 1) nueva_cabeza.y = 1;

        snake.push_front(nueva_cabeza);

        gotoxy(nueva_cabeza.x + OFFSET_X, nueva_cabeza.y + OFFSET_Y);
        setColor(AZUL_CLARO);
        cout << "O";

        bool consumio_premio = (nueva_cabeza == premio);
        bool consumio_trampa = false;

        Coordenada trampa_consumida = { 0, 0 };
        for (const auto& trampa_pos : trampas) {
            if (nueva_cabeza == trampa_pos) {
                consumio_trampa = true;
                trampa_consumida = trampa_pos;
                break;
            }
        }

        if (!consumio_premio && !consumio_trampa) {
            Coordenada cola_anterior = snake.back();
            snake.pop_back();

            gotoxy(cola_anterior.x + OFFSET_X, cola_anterior.y + OFFSET_Y);
            cout << " ";

            if (snake.size() > 1) {
                auto it_penultimo = snake.begin();
                std::advance(it_penultimo, snake.size() - 2);
                gotoxy(it_penultimo->x + OFFSET_X, it_penultimo->y + OFFSET_Y);
                setColor(AZUL);
                cout << "o";
            }

        }
        else if (consumio_premio) {
            puntuacion += PTS_PREMIO;
            premio = { 0, 0 };
            generarAmbosElementos();
            chequearNivel();

        }
        else if (consumio_trampa) {
            puntuacion += PTS_TRAMPA;

            if (snake.size() > 0) {
                Coordenada cola_segmento_1 = snake.back();
                snake.pop_back();
                gotoxy(cola_segmento_1.x + OFFSET_X, cola_segmento_1.y + OFFSET_Y);
                cout << " ";
            }
            if (snake.size() > 0) {
                Coordenada cola_segmento_2 = snake.back();
                snake.pop_back();
                gotoxy(cola_segmento_2.x + OFFSET_X, cola_segmento_2.y + OFFSET_Y);
                cout << " ";
            }

            generarAmbosElementos();
        }

        dibujarHUD();
        setColor(BLANCO);
    }

    void chequearNivel() {
        if (snake.size() >= LONGITUD_LIMITE_NIVEL) {
            nivel++;
            puntuacion += PTS_NIVEL;
            calcularVelocidad();

            Coordenada cabeza_actual = snake.front();

            for (const auto& segment : snake) {
                gotoxy(segment.x + OFFSET_X, segment.y + OFFSET_Y);
                cout << " ";
            }
            snake.clear();

            for (int i = 0; i < LONGITUD_INICIAL; ++i) {
                snake.push_back({ cabeza_actual.x, cabeza_actual.y - i });
            }

            dibujarFronteras();
            gotoxy(cabeza_actual.x + OFFSET_X, cabeza_actual.y + OFFSET_Y);
            setColor(AZUL_CLARO);
            cout << "O";

            generarAmbosElementos();
            dibujarHUD();

            gotoxy(OFFSET_X + ANCHO_TABLERO / 2 - 5, ALTO_TABLERO + OFFSET_Y + 4);
            setColor(VERDE_CLARO);
            cout << "¡NIVEL " << nivel << "!";
            SleepMs(1000);
            gotoxy(OFFSET_X + ANCHO_TABLERO / 2 - 5, ALTO_TABLERO + OFFSET_Y + 4);
            cout << "              ";
        }
    }

    bool chequearColisiones() {
        if (snake.size() <= 0) return true;

        const Coordenada& cabeza = snake.front();

        auto it = snake.begin();
        it++;
        while (it != snake.end()) {
            if (cabeza == *it) {
                return true;
            }
            it++;
        }

        return false;
    }



    std::vector<Record> cargarRanking() {
        std::vector<Record> ranking;
        std::ifstream archivo(RANKING_FILE);
        std::string linea, nombre_str, puntuacion_str;

        while (std::getline(archivo, linea)) {
            std::stringstream ss(linea);
            if (std::getline(ss, nombre_str, ',') &&
                std::getline(ss, puntuacion_str)) {
                try {
                    ranking.push_back({ nombre_str, std::stoi(puntuacion_str) });
                }
                catch (...) {}
            }
        }
        return ranking;
    }

    void guardarRanking(const std::vector<Record>& ranking) {
        std::ofstream archivo(RANKING_FILE);
        int count = 0;
        for (const auto& record : ranking) {
            if (count++ >= 5) break;
            archivo << record.nombre << "," << record.puntuacion << std::endl;
        }
    }

    static bool compararRecords(const Record& a, const Record& b) {
        return a.puntuacion > b.puntuacion;
    }

    void actualizarRanking(const string& nombre_jugador) {
        std::vector<Record> ranking = cargarRanking();
        ranking.push_back({ nombre_jugador, puntuacion });
        std::sort(ranking.begin(), ranking.end(), compararRecords);
        guardarRanking(ranking);
    }

    void mostrarRanking() {
        CLEAR_SCREEN;
        setColor(AZUL);
        gotoxy(CONSOLA_ANCHO_MAX / 2 - 12, 5);
        cout << " TOP 5 RANKING (Puntuacion) ";
        gotoxy(CONSOLA_ANCHO_MAX / 2 - 15, 7);
        cout << "------------------------------------";

        setColor(BLANCO_BRILLANTE);

        std::vector<Record> ranking = cargarRanking();
        if (ranking.empty()) {
            gotoxy(CONSOLA_ANCHO_MAX / 2 - 10, 9);
            cout << "No hay records aun.";
        }
        else {
            for (size_t i = 0; i < std::min((size_t)5, ranking.size()); ++i) {
                gotoxy(CONSOLA_ANCHO_MAX / 2 - 15, 9 + i);
                cout << i + 1 << ". " << std::setw(15) << std::left << ranking[i].nombre
                    << " - Pts: " << ranking[i].puntuacion;
            }
        }

        gotoxy(CONSOLA_ANCHO_MAX / 2 - 10, 20);
        cout << "Presiona cualquier tecla para volver al menu...";
        (void)_getch();
        setColor(BLANCO);
    }


public:
    SnakeGame()
        : dir(PARAR), puntuacion(0), nivel(1), velocidad_ms(VELOCIDAD_BASE_MS),
        premio({ 0, 0 }), partida_cargada(false) {
        srand(static_cast<unsigned int>(time(0)));
    }

    void iniciarNuevaPartida(bool cargada = false) {

        if (!cargada) {
            snake.clear();
            inicializarSerpiente();
            puntuacion = 0;
            nivel = 1;
            calcularVelocidad();
            dir = PARAR;
            premio = { 0, 0 };
        }
        else {
            if (dir == PARAR) dir = ARRIBA;
        }

        bool game_over = false;

        dibujarFronteras();
        if (cargada) {
            dibujarSerpiente();
        }
        generarAmbosElementos();
        dibujarHUD();

        while (dir == PARAR) {
            if (_kbhit()) {
                char key = _getch();
                switch (key) {
                case 'w': case 'W': dir = ARRIBA; break;
                case 'a': case 'A': dir = IZQUIERDA; break;
                case 's': case 'S': dir = ABAJO; break;
                case 'd': case 'D': dir = DERECHA; break;
                case 'g': case 'G':
                    dir = PARAR;
                    guardarJuego();
                    return;
                case 'x': case 'X': return;
                }
            }
            SleepMs(50);
        }

        while (!game_over && dir != PARAR) {
            input();
            if (dir == PARAR) return;

            moverSerpiente();
            game_over = chequearColisiones();
            SleepMs(velocidad_ms);
        }

        CLEAR_SCREEN;
        setColor(ROJO_CLARO);
        gotoxy(CONSOLA_ANCHO_MAX / 2 - 7, CONSOLA_ALTO_MAX / 2 - 2);
        cout << "¡GAME OVER!";
        setColor(BLANCO_BRILLANTE);
        gotoxy(CONSOLA_ANCHO_MAX / 2 - 10, CONSOLA_ALTO_MAX / 2 - 1);
        cout << "Puntuacion Final: " << puntuacion;
        gotoxy(CONSOLA_ANCHO_MAX / 2 - 10, CONSOLA_ALTO_MAX / 2);
        cout << "Nivel Alcanzado: " << nivel;

        if (puntuacion > 0) {
            actualizarRanking(nombre_jugador);
        }

        gotoxy(CONSOLA_ANCHO_MAX / 2 - 10, CONSOLA_ALTO_MAX / 2 + 3);
        cout << "Presiona cualquier tecla para ir al menu...";
        (void)_getch();
        setColor(BLANCO);
    }

    void startGame() {
        string nombre_input;
        CLEAR_SCREEN;

        gotoxy(CONSOLA_ANCHO_MAX / 2 - 15, CONSOLA_ALTO_MAX / 2 - 2);
        setColor(BLANCO_BRILLANTE);
        cout << "Ingresa tu nombre para el ranking (sin espacios): ";
        cin >> nombre_input;
        nombre_jugador = nombre_input;

        iniciarNuevaPartida(false);
    }

    void loadGame() {
        if (cargarJuego()) {
            CLEAR_SCREEN;
            string nombre_input;
            gotoxy(CONSOLA_ANCHO_MAX / 2 - 15, CONSOLA_ALTO_MAX / 2 - 2);
            setColor(CYAN_CLARO);
            cout << "Partida cargada exitosamente. Ingresa tu nombre: ";
            cin >> nombre_input;
            nombre_jugador = nombre_input;

            iniciarNuevaPartida(true);
        }
        else {
            CLEAR_SCREEN;
            setColor(ROJO_CLARO);
            gotoxy(CONSOLA_ANCHO_MAX / 2 - 15, CONSOLA_ALTO_MAX / 2);
            cout << "ERROR: No se encontró una partida guardada.";
            gotoxy(CONSOLA_ANCHO_MAX / 2 - 15, CONSOLA_ALTO_MAX / 2 + 2);
            cout << "Presiona cualquier tecla para volver al menu...";
            (void)_getch();
        }
    }

    void menu() {
        int opcion;
        do {
            CLEAR_SCREEN;
            setColor(AZUL);
            gotoxy(CONSOLA_ANCHO_MAX / 2 - 8, 5);
            cout << "--- SNAKE BY CHAYO ---";

            setColor(BLANCO_BRILLANTE);
            gotoxy(CONSOLA_ANCHO_MAX / 2 - 8, 7);
            cout << "1. Jugar Partida Nueva";
            gotoxy(CONSOLA_ANCHO_MAX / 2 - 8, 8);
            cout << "2. Cargar Partida";
            gotoxy(CONSOLA_ANCHO_MAX / 2 - 8, 9);
            cout << "3. Ver Ranking";
            gotoxy(CONSOLA_ANCHO_MAX / 2 - 8, 10);
            cout << "4. Salir";
            gotoxy(CONSOLA_ANCHO_MAX / 2 - 8, 12);
            cout << "Elige una opcion: ";

            if (!(cin >> opcion)) {
                cin.clear();
                cin.ignore(10000, '\n');
                opcion = 0;
            }

            switch (opcion) {
            case 1:
                startGame();
                break;
            case 2:
                loadGame();
                break;
            case 3:
                mostrarRanking();
                break;
            case 4:
                break;
            default:
                setColor(ROJO);
                gotoxy(CONSOLA_ANCHO_MAX / 2 - 8, 14);
                cout << "Opcion invalida. Intenta de nuevo.";
                SleepMs(1500);
                setColor(BLANCO);
            }
        } while (opcion != 4);
    }
};



int main() {
    SnakeGame game;
    game.menu();
    return 0;
}