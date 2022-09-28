# Семейство скриптов компонента Afterburner

## [Afterburner](./../../Runtime/Code/Afterburner/Afterburner.cs)

Управление тягой двигателя в экстремальном режиме.

### Какие действия выполняет

При активации, устанавливает тягу двигателя на заданную, в продолжении указанного времени. По истечению времени активации, выполняется перезарядка компонента с указанной продолжительнотью, в течении которой, повторная активация компонента не возможна. Если в процессе работы, тягу [Flight](./../../Runtime/Code/Flight/Flight.cs) изменить сторонними средствами, компонент прервет свою работу и уйдет в перезарядку.

### Свойства компонента

- float throttle - максимальная тяга во время активации
- float duration - длительность активации
- float cooldown - продолжительность восстановления

**Реагирует на следующие события:**

```c#
// Активация кмпонента
OnAfterburnerActivation(bool activation)
```

**Создает следующие сквозные события:**

```c#
// Нет предусмотренных событий
```

## [AutoAfterburner](./../../Runtime/Code/Afterburner/AutoAfterburner.cs)

Автоматизация активации [Afterburner](./../../Runtime/Code/Afterburner/Afterburner.cs).

### Какие действия выполняет

Обрабатывает события входа в зону досягаемости и выравниания на цель. При соблюдении обоих условий, активирует [Afterburner](./../../Runtime/Code/Afterburner/Afterburner.cs).

> Данный компонент наследуется от [AbstractReaction](https://github.com/freak-games/core/blob/main/docs/ru/abstract-components.md#abstractreaction)

### Свойства компонента

- отсутсвтуют

**Реагирует на следующие события:**

```c#
// Выравнивание на цель
OnAlignment(bool inAlignment)

// Вход в зону досягаемости
OnRange(bool inRange)
```

**Создает следующие сквозные события:**

```c#
// Активация Afterburner
OnAfterburnerActivation(bool activation)
```

## Набор компонентов для перенаправления событий

> Данная группа компонентов наследуется от [AbstractEventCollector](https://github.com/freak-games/core/blob/main/docs/ru/abstract-components.md#abstracteventcollector) и [AbstractEventEmitter](https://github.com/freak-games/core/blob/main/docs/ru/abstract-components.md#abstracteventemitter)

[AfterburnerActivationEventCollector](./../../Runtime/Code/Afterburner/AfterburnerActivationEventCollector.cs) и [AfterburnerActivationEventEmitter](./../../Runtime/Code/Afterburner/AfterburnerActivationEventEmitter.cs)

[AfterburnerEventCollector](./../../Runtime/Code/Afterburner/AfterburnerEventCollector.cs) и [AfterburnerEventEmitter](./../../Runtime/Code/Afterburner/AfterburnerEventEmitter.cs)

## Набор компонентов для отображения характеристик в текстовом виде

> Данная группа компонентов наследуется от [AbstractTextMeshProDisplayController и AbstractTextMeshProNumericalDisplayController](https://github.com/freak-games/core/blob/main/docs/ru/abstract-components.md#abstracttextmeshprodisplaycontroller-и-abstracttextmeshpronumericaldisplaycontroller)

[AfterburnerThrottleDisplayController](./../../Runtime/Code/Afterburner/AfterburnerThrottleDisplayController.cs)

[AfterburnerDurationDisplayController](./../../Runtime/Code/Afterburner/AfterburnerDurationDisplayController.cs)

[AfterburnerCooldownDisplayController](./../../Runtime/Code/Afterburner/AfterburnerCooldownDisplayController.cs)
