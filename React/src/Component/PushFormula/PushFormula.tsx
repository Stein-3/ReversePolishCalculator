import React, { useState } from "react";
import { useRecoilState } from "recoil";
import {
  clearStack,
  displayStack,
  executeFormula,
  popFormula,
  pushFormula,
} from "../../Common/RPNApiCaller";
import { StackState } from "../../Store/StackAtom";
import Presenter from "./Presenter";

const PushFormula: React.FC = () => {
  const SUCCESS_MESSAGE = "成功しました";

  const [, setStack] = useRecoilState(StackState);

  const [popResult, setPopResult] = useState("");
  const [clearResult, setClearResult] = useState("");
  const [runResult, setRunResult] = useState("");
  const [content, setContent] = useState<string>("");

  const onPopButtonClick = (e: React.SyntheticEvent) => {
    e.preventDefault();
    initResult();

    popFormula()
      .then(async (result) => {
        setPopResult(result);
        await updateStack();
      })
      .catch((e) => {
        setPopResult(
          `Stackからの値の取り出しに失敗しました。${(e as Error).message}`
        );
      });
  };

  const onClearButtonClick = (e: React.SyntheticEvent) => {
    e.preventDefault();
    initResult();

    clearStack()
      .then(async () => {
        setClearResult(SUCCESS_MESSAGE);
        await updateStack();
      })
      .catch((e) => {
        setClearResult(`Stackの初期化に失敗しました。${e.message}`);
      });
  };

  const onRunButtonClick = (e: React.SyntheticEvent) => {
    e.preventDefault();
    initResult();

    executeFormula()
      .then(async () => {
        setRunResult(SUCCESS_MESSAGE);
        await updateStack();
      })
      .catch((e) => {
        setRunResult(`実行に失敗しました。${e.message}`);
      });
  };

  const updateStack = async () => {
    try {
      const newStack = await displayStack();
      setStack(newStack);
    } catch (e) {
      setStack(["Stackの更新に失敗しました"]);
    }
  };

  const onPushButtonClick = (e: React.SyntheticEvent) => {
    e.preventDefault();
    pushFormula(content)
      .then(async () => {
        try {
          const newStack = await displayStack();
          setStack(newStack);
        } catch (e) {
          window.alert(
            `Stackの読み込みに失敗しました。${(e as Error).message}`
          );
        }
        setContent("");
      })
      .catch((e) => {
        window.alert(`式の送信に失敗しました。${e.message}`);
      });
  };

  const onPushInputboxChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setContent(() => e.target.value);
  };

  const initResult = () => {
    setClearResult("");
    setPopResult("");
    setRunResult("");
  };

  return (
    <Presenter
      push={{
        content: content,
        onButtonClick: onPushButtonClick,
        onInputChange: onPushInputboxChange,
      }}
      pop={{
        result: popResult,
        onClickFunc: onPopButtonClick,
      }}
      clear={{
        result: clearResult,
        onClickFunc: onClearButtonClick,
      }}
      run={{
        result: runResult,
        onClickFunc: onRunButtonClick,
      }}
    />
  );
};

export default PushFormula;
