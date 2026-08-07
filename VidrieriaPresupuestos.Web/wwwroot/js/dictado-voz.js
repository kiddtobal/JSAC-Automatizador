let reconocimiento = null;

window.iniciarDictado = (dotNetRef) => {
    const SpeechRecognitionApi = window.SpeechRecognition || window.webkitSpeechRecognition;

    if (!SpeechRecognitionApi) {
        return false;
    }

    if (reconocimiento) {
        reconocimiento.stop();
        reconocimiento = null;
    }

    reconocimiento = new SpeechRecognitionApi();
    reconocimiento.lang = 'es-CL';
    reconocimiento.continuous = true;
    reconocimiento.interimResults = false;

    reconocimiento.onresult = (event) => {
        for (let i = event.resultIndex; i < event.results.length; i++) {
            if (event.results[i].isFinal) {
                const texto = event.results[i][0].transcript.trim();
                if (texto) {
                    dotNetRef.invokeMethodAsync('AgregarFragmentoDictado', texto);
                }
            }
        }
    };

    reconocimiento.onerror = (event) => {
        dotNetRef.invokeMethodAsync('OnErrorDictado', event.error);
    };

    reconocimiento.onend = () => {
        dotNetRef.invokeMethodAsync('OnFinDictado');
    };

    reconocimiento.start();
    return true;
};

window.detenerDictado = () => {
    if (reconocimiento) {
        reconocimiento.stop();
        reconocimiento = null;
    }
};
