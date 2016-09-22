#define PONTO_API __declspec(dllimport)

/*
 * This class is exported from the Ponto.dll
 */
class PONTO_API Ponto {
public:
	int _x, _y;
	Ponto(int x, int y);
	double Ponto::getModule();
	void Ponto::print();
};
