# Семейство скриптов компонента OverG

## [OverG](./../../Runtime/Code/OverG/OverG.cs)

Расчет перегрузки пилота.

### Какие действия выполняет

Анализирует вертикальную скорость `Rigidbody` и расчитывает значение нагрузки. Когда нагрузка превышает указанный порог, пилот начинает испытывать перегрузку, что приводит к уменьшению его выносливости. Как только выносливость пилота будет <= 0, компонент зарегестрирует потерю сознания пилота. Такое состояние будет длиться пока выносливать пилота не востановится до положительных значений.

### Свойства компонента

- float threshold - порог нагрузки, который пилот выдерживает без последствий
- float stamina - выносливость пилота
- float g - фактическая нагрузка
- float overG - велечина прегрузки (g - threshold)
- bool gloc - флаг потери сознания пилотом

**Реагирует на следующие события:**

```c#
// Полчение повреждений увеличивает перегрузку пилота
OnDamage(object[] damage)
```

**Создает следующие сквозные события:**

```c#
// Пилот потерял сознание
OnGloc(bool gloc)
```

## [OverGNoiseAudioController](./../../Runtime/Code/OverG/OverGNoiseAudioController.cs)

Управляет воспроизведением звука шума от перегрузки.

### Какие действия выполняет

Отслеживает текущее значение перегрузки [OverG](./../../Runtime/Code/OverG/OverG.cs) увеличивает громкость шума по мере ее приближения к маскимальным значениям.

> Данный компонент наследуется от [AbstractAudioController](https://github.com/freak-games/core/blob/main/docs/ru/abstract-components.md#abstractaudiocontroller)

### Свойства компонента

- GameObject target - целевой объект, содержащий компонент [OverG](./../../Runtime/Code/OverG/OverG.cs)
- AnimationCurve volume - кривая громкости звука по отношению к текущей перегузке, где 1 - максимальная перегузка

**Реагирует на следующие события:**

```c#
// Сбрасывает целевой объект
OnControlTargetBreak()

// Устанавливает целевой объект
OnControlTargetSet(GameObject target)
```

**Создает следующие сквозные события:**

```c#
// Нет предусмотренных событий
```

## [OverGRingingAudioController](./../../Runtime/Code/OverG/OverGRingingAudioController.cs)

Управляет воспроизведением звука при восстановлении от перегрузки.

### Какие действия выполняет

Отслеживает текущее значение перегрузки [OverG](./../../Runtime/Code/OverG/OverG.cs) и если перегрузка превышает указанный порог, то при обратном его прохождении, во время восстановления, компонент воспроизведет звук.

> Данный компонент наследуется от [AbstractAudioController](https://github.com/freak-games/core/blob/main/docs/ru/abstract-components.md#abstractaudiocontroller)

### Свойства компонента

- GameObject target - целевой объект, содержащий компонент [OverG](./../../Runtime/Code/OverG/OverG.cs)
- float threshold - порог срабатывания

**Реагирует на следующие события:**

```c#
// Сбрасывает целевой объект
OnControlTargetBreak()

// Устанавливает целевой объект
OnControlTargetSet(GameObject target)
```

**Создает следующие сквозные события:**

```c#
// Нет предусмотренных событий
```

## [OverGMixerFilterController](./../../Runtime/Code/OverG/OverGMixerFilterController.cs)

Управляет эффектом `AudioMixer`.

### Какие действия выполняет

Отслеживает текущее значение перегрузки [OverG](./../../Runtime/Code/OverG/OverG.cs) и меняет значение указанного фильтра в заданном `AudioMixer`.

### Свойства компонента

- GameObject target - целевой объект, содержащий компонент [OverG](./../../Runtime/Code/OverG/OverG.cs)
- AudioMixer mixer - микшер
- string filterLabel - наименование свойства фильтра
- AnimationCurve filter - кривая частоты фильтра по отношению к перегрузке, где 1 - максимальная перегрузка

**Реагирует на следующие события:**

```c#
// Сбрасывает целевой объект
OnControlTargetBreak()

// Устанавливает целевой объект
OnControlTargetSet(GameObject target)

// Отключает компонент
OnRuined()
```

**Создает следующие сквозные события:**

```c#
// Нет предусмотренных событий
```

## Набор компонентов для перенаправления событий

> Данная группа компонентов наследуется от [AbstractEventCollector](https://github.com/freak-games/core/blob/main/docs/ru/abstract-components.md#abstracteventcollector) и [AbstractEventEmitter](https://github.com/freak-games/core/blob/main/docs/ru/abstract-components.md#abstracteventemitter)

[GlocEventCollector](./../../Runtime/Code/OverG/GlocEventCollector.cs) и [GlocEventEmitter](./../../Runtime/Code/OverG/GlocEventEmitter.cs)

## Набор компонентов для отображения характеристик в текстовом виде

> Данная группа компонентов наследуется от [AbstractTextMeshProDisplayController и AbstractTextMeshProNumericalDisplayController](https://github.com/freak-games/core/blob/main/docs/ru/abstract-components.md#abstracttextmeshprodisplaycontroller-и-abstracttextmeshpronumericaldisplaycontroller)

[OverGGDisplayController](./../../Runtime/Code/OverG/OverGGDisplayController.cs)

[OverGDisplayController](./../../Runtime/Code/OverG/OverGDisplayController.cs)

[OverGStaminaDisplayController](./../../Runtime/Code/OverG/OverGStaminaDisplayController.cs)

[OverGThresholdDisplayController](./../../Runtime/Code/OverG/OverGThresholdDisplayController.cs)