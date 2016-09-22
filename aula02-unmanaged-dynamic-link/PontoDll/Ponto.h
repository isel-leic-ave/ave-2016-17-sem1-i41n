#define PONTO_API __declspec(dllexport)

/*
 * This class is exported from the Ponto.dll
 */
class PONTO_API Ponto {
public:
	int _y, _x;
	Ponto(int x, int y);
	double Ponto::getModule();
	void Ponto::print();
};
